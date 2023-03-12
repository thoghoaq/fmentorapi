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
            ReviewReviewees = new HashSet<Review>();
            ReviewReviewers = new HashSet<Review>();
            UserSpecialties = new HashSet<UserSpecialty>();
            Wallets = new HashSet<Wallet>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }
        public string Description { get; set; } = null!;
        public string VideoIntroduction { get; set; } = null!;
        public byte IsMentor { get; set; }
        public string? Photo { get; set; }

        public virtual UserPermission IsMentorNavigation { get; set; } = null!;
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Mentee> Mentees { get; set; }
        public virtual ICollection<Mentor> Mentors { get; set; }
        public virtual ICollection<Review> ReviewReviewees { get; set; }
        public virtual ICollection<Review> ReviewReviewers { get; set; }
        public virtual ICollection<UserSpecialty> UserSpecialties { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
