using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DataAccess.Models
{
    public partial class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [StringLength(255, ErrorMessage = "Note cannot be longer than 255 characters.")]
        public string? Note { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
