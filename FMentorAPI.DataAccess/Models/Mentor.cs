namespace FMentorAPI.DataAccess.Models
{
    public partial class Mentor
    {
        public Mentor()
        {
            Appointments = new HashSet<Appointment>();
            Bookings = new HashSet<Booking>();
            Courses = new HashSet<Course>();
            MentorAvailabilities = new HashSet<MentorAvailability>();
            MentorWorkingTimes = new HashSet<MentorWorkingTime>();

            FollowedMentors = new HashSet<FollowedMentor>();

            Mentees = new HashSet<Mentee>();

        }

        public int MentorId { get; set; }
        public int UserId { get; set; }
        public string Specialty { get; set; }
        public decimal HourlyRate { get; set; }
        public byte Availability { get; set; }


        public virtual User User { get; set; } = null!;
        public virtual Ranking? Ranking { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<MentorAvailability> MentorAvailabilities { get; set; }
        public virtual ICollection<MentorWorkingTime> MentorWorkingTimes { get; set; }

        public virtual ICollection<FollowedMentor> FollowedMentors { get; set; }

        public virtual ICollection<Mentee> Mentees { get; set; }

    }
}
