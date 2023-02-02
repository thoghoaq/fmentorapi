using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class MentorWorkingTime
    {
        [Key]
        public int MentorId { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Day of week cannot be longer than 10 characters.")]
        public string DayOfWeek { get; set; } = null!;
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }

        public virtual Mentor Mentor { get; set; } = null!;
    }
}
