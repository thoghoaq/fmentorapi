using FMentorAPI.DataAccess.Models;

namespace FMentorAPI.BusinessLogic.Services;

public interface IPaymentService
{
}

public class PaymentService : IPaymentService
{
    private readonly FMentorDBContext _context;

    public PaymentService(FMentorDBContext context)
    {
        _context = context;
    }
}