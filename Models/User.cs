using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class User
    {
        public User()
        {
            AppointmentMentees = new HashSet<Appointment>();
            AppointmentMentors = new HashSet<Appointment>();
            Educations = new HashSet<Education>();
            Jobs = new HashSet<Job>();
            Mentees = new HashSet<Mentee>();
            MentorAvailabilities = new HashSet<MentorAvailability>();
            Mentors = new HashSet<Mentor>();
            ReviewReviewees = new HashSet<Review>();
            ReviewReviewers = new HashSet<Review>();
            UserSpecialties = new HashSet<UserSpecialty>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int Age { get; set; }
        public string Description { get; set; } = null!;
        public string VideoIntroduction { get; set; } = null!;

        public virtual UserPermission? UserPermission { get; set; }
        public virtual ICollection<Appointment> AppointmentMentees { get; set; }
        public virtual ICollection<Appointment> AppointmentMentors { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Mentee> Mentees { get; set; }
        public virtual ICollection<MentorAvailability> MentorAvailabilities { get; set; }
        public virtual ICollection<Mentor> Mentors { get; set; }
        public virtual ICollection<Review> ReviewReviewees { get; set; }
        public virtual ICollection<Review> ReviewReviewers { get; set; }
        public virtual ICollection<UserSpecialty> UserSpecialties { get; set; }
    }
}
