using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DataAccess.Models
{
    public partial class MentorAvailability
    {
        [Key]
        public int MentorId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public virtual Mentor Mentor { get; set; } = null!;
    }
}
