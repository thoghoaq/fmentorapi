using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class Mentee
    {
        public Mentee()
        {
            Bookings = new HashSet<Booking>();
            Courses = new HashSet<Course>();
        }

        public int MenteeId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
