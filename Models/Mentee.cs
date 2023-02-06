using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class Mentee
    {
        public Mentee()
        {
            Bookings = new HashSet<Booking>();
            Appointments = new HashSet<Appointment>();
            Courses = new HashSet<Course>();
            Mentors = new HashSet<Mentor>();
        }
        [Key]
        public int MenteeId { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Mentor> Mentors { get; set; }
    }
}
