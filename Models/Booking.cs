using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int MentorId { get; set; }
        public int MenteeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; } = null!;
        public string? ReasonForRejection { get; set; }

        public virtual Mentee Mentee { get; set; } = null!;
        public virtual Mentor Mentor { get; set; } = null!;
    }
}
