using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DataAccess.Models
{
    public partial class Course
    {
        public Course()
        {
            Mentees = new HashSet<Mentee>();
        }
        [Key]
        public int CourseId { get; set; }
        [Required]
        public int MentorId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(100, ErrorMessage = "Instructor name cannot be longer than 100 characters.")]
        public string Instructor { get; set; } = null!;
        [Required]
        [StringLength(100, ErrorMessage = "Platform cannot be longer than 100 characters.")]
        public string Platform { get; set; } = null!;
        [Required]
        [StringLength(255, ErrorMessage = "Link cannot be longer than 255 characters.")]
        public string Link { get; set; } = null!;
        [Required]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; } = null!;
        [StringLength(255, ErrorMessage = "Photo URL cannot be longer than 255 characters.")]
        public string Photo { get; set; } = null!;

        public virtual Mentor Mentor { get; set; } = null!;

        public virtual ICollection<Mentee> Mentees { get; set; }
        public virtual ICollection<FavoriteCourse> FavoriteCourses { get; set; }
    }
}
