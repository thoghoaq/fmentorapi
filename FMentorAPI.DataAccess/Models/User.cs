using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            Educations = new HashSet<Education>();
            Jobs = new HashSet<Job>();
            Mentees = new HashSet<Mentee>();
            Mentors = new HashSet<Mentor>();
            Payments = new HashSet<Payment>();
            ReviewReviewees = new HashSet<Review>();
            ReviewReviewers = new HashSet<Review>();
            UserSpecialties = new HashSet<UserSpecialty>();
        }
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Role is required")]
        public byte IsMentor { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Age must be a positive number")]
        public int Age { get; set; }
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; } = null!;
        [StringLength(255, ErrorMessage = "Video introduction link cannot be longer than 255 characters.")]
        public string VideoIntroduction { get; set; } = null!;
        [StringLength(255, ErrorMessage = "Photo link cannot be longer than 255 characters.")]
        public string? Photo { get; set; }

        public virtual UserPermission IsMentorNavigation { get; set; } = null!;
        public virtual Wallet? Wallet { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Mentee> Mentees { get; set; }
        public virtual ICollection<Mentor> Mentors { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Review> ReviewReviewees { get; set; }
        public virtual ICollection<Review> ReviewReviewers { get; set; }
        public virtual ICollection<UserSpecialty> UserSpecialties { get; set; }
    }
}
