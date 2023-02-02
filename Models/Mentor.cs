using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class Mentor
    {
        public Mentor()
        {
            Bookings = new HashSet<Booking>();
            Courses = new HashSet<Course>();
            MentorWorkingTimes = new HashSet<MentorWorkingTime>();
        }

        public int MentorId { get; set; }
        public int UserId { get; set; }
        public string Specialty { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public byte Availability { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<MentorWorkingTime> MentorWorkingTimes { get; set; }
    }
}
