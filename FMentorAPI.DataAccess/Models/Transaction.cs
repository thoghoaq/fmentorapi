namespace FMentorAPI.DataAccess.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public decimal? Amount { get; set; }
        public int? WalletId { get; set; }
        public int? PaymentId { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
