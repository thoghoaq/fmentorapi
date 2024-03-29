﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMentorAPI.DataAccess.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Reviews = new HashSet<Review>();
        }
        [Key]
        public int AppointmentId { get; set; }
        [Required]
        public int MentorId { get; set; }
        [Required]
        public int MenteeId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Google Meet Link cannot be longer than 255 characters.")]
        public string GoogleMeetLink { get; set; } = null!;
        public string Password { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be greater than 0.")]
        public int Duration { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Status cannot be longer than 50 characters.")]
        public string Status { get; set; } = null!;
        [StringLength(500, ErrorMessage = "Note cannot be longer than 500 characters.")]
        public string? Note { get; set; }
        [Column("is_reviewed")]
        public bool IsReviewed { get; set; } = false;

        public virtual Mentee Mentee { get; set; } = null!;
        public virtual Mentor Mentor { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
