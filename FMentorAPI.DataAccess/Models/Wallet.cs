using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DataAccess.Models
{
    public partial class Wallet
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Balance must be greater than 0.")]
        public decimal Balance { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
