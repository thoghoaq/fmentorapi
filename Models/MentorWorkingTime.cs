using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class MentorWorkingTime
    {
        public int MentorId { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual Mentor Mentor { get; set; } = null!;
    }
}
