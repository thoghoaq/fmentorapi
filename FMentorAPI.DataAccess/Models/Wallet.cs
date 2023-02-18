namespace FMentorAPI.DataAccess.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Payments = new HashSet<Payment>();
            Transactions = new HashSet<Transaction>();
        }

        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public int WalletId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
