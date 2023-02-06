using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [Required]
        public int MentorId { get; set; }
        [Required]
        public int MenteeId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total cost must be greater than or equal to 0.")]
        public decimal TotalCost { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Status cannot be longer than 50 characters.")]
        public string Status { get; set; } = null!;
        [StringLength(500, ErrorMessage = "Reason for rejection cannot be longer than 500 characters.")]
        public string? ReasonForRejection { get; set; }

        public virtual Mentee Mentee { get; set; } = null!;
        public virtual Mentor Mentor { get; set; } = null!;
    }
}
