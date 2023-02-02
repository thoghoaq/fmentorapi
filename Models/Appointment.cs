using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Reviews = new HashSet<Review>();
        }

        public int AppointmentId { get; set; }
        public int MentorId { get; set; }
        public int MenteeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string GoogleMeetLink { get; set; } = null!;
        public int Duration { get; set; }
        public string Status { get; set; } = null!;
        public string? Note { get; set; }

        public virtual User Mentee { get; set; } = null!;
        public virtual User Mentor { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
