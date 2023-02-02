using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class Education
    {
        public int EducationId { get; set; }
        public int UserId { get; set; }
        public string School { get; set; } = null!;
        public string Major { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
