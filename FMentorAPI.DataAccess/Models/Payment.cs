namespace FMentorAPI.DataAccess.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int PaymentId { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Note { get; set; }

        public virtual Wallet Wallet { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
