using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DataAccess.Models
{
    public partial class UserSpecialty
    {
        [Key]
        public int UserSpecialtyId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
