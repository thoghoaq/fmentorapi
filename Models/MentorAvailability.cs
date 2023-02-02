using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class MentorAvailability
    {
        public int MentorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual User Mentor { get; set; } = null!;
    }
}
