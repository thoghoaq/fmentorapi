using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DataAccess.Models
{
    public partial class Ranking
    {
        [Key]
        public int MentorId { get; set; }
        [StringLength(50, ErrorMessage = "Rank cannot be longer than 50 characters.")]
        public string? Rank { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Point must be a non-negative integer.")]
        public int? Point { get; set; }

        public virtual Mentor Mentor { get; set; } = null!;
    }
}
