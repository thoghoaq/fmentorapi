using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class Job
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string Company { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
