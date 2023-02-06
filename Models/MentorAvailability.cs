using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class MentorAvailability
    {
        [Key]
        public int MentorId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public virtual Mentor Mentor { get; set; } = null!;
    }
}
