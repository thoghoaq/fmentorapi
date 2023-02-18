using FMentorAPI.BusinessLogic.DTOs;
using FMentorAPI.BusinessLogic.DTOs.RequestModel;
using FMentorAPI.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace FMentorAPI.BusinessLogic.Services;

public interface IPaymentService
{
    DonateResponseModel CreateDonate(DonateRequestModel donateRequestModel);
}

public class PaymentService : IPaymentService
{
    private readonly FMentorDBContext _context;

    public PaymentService(FMentorDBContext context)
    {
        _context = context;
    }

    public DonateResponseModel CreateDonate(DonateRequestModel donateRequestModel)
    {
        #region Get infomation

        var receiver = _context.Users.Include(x => x.Wallets)
            .SingleOrDefault(x => x.UserId == donateRequestModel.ReceiverId);

        if (receiver == null)
        {
            return new DonateResponseModel()
            {
                Message = "Receiver is not found!"
            };
        }

        var sender = _context.Users.Include(x => x.Wallets)
            .SingleOrDefault(x => x.UserId == donateRequestModel.SenderId);

        if (sender == null)
        {
            return new DonateResponseModel()
            {
                Message = "Sender is not found!"
            };
        }

        var senderWallet = sender.Wallets.First();
        var receiverWallet = receiver.Wallets.First();

        #endregion

        // Check receiver balance with amount donation
        if (Math.Round(donateRequestModel.Amount, 4) > senderWallet.Balance)
        {
            return new DonateResponseModel()
            {
                Message = "Sender's balance is not enough"
            };
        }

        // Create payment
        Payment payment = new Payment()
        {
            Amount = donateRequestModel.Amount,
            Note = donateRequestModel.Description ?? $"[{sender.Name} donate {receiver.Name}] Amount: {donateRequestModel.Amount}",
            WalletId = senderWallet.WalletId,
            PaymentDate = DateTime.Now,
            Transactions = new List<Transaction>()
            {
                // Create transaction for receiver
                new Transaction()
                {
                    Amount = donateRequestModel.Amount,
                    WalletId = receiverWallet.WalletId,
                },
                // Create transaction for sender
                new Transaction()
                {
                    Amount = -donateRequestModel.Amount,
                    WalletId = senderWallet.WalletId,
                }
            }
        };

        var result = _context.Payments.Add(payment);

        #region Update Wallet

        // Sender
        senderWallet.Balance -= Math.Round(donateRequestModel.Amount, 4);
        _context.Update(senderWallet);
        // Receiver
        receiverWallet.Balance += Math.Round(donateRequestModel.Amount, 4);
        _context.Update(receiverWallet);

        #endregion

        _context.SaveChanges();

        return new DonateResponseModel()
        {
            Message = "Success"
        };
    }
}