using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMentorAPI.Models
{
    public partial class Mentor
    {
        public Mentor()
        {
            Appointments = new HashSet<Appointment>();
            Bookings = new HashSet<Booking>();
            Courses = new HashSet<Course>();
            MentorWorkingTimes = new HashSet<MentorWorkingTime>();
            FollowedMentors = new HashSet<FollowedMentor>();
        }

        public int MentorId { get; set; }
        public int UserId { get; set; }
        public string Specialty { get; set; }
        public decimal HourlyRate { get; set; }
        public byte Availability { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<MentorWorkingTime> MentorWorkingTimes { get; set; }

        public virtual ICollection<FollowedMentor> FollowedMentors { get; set; }
    }
}
