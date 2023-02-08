using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMentorAPI.Models
{
    public partial class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        public int ReviewerId { get; set; }
        [Required]
        public int RevieweeId { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        [Column("is_reviewed")]
        public bool IsReviewed { get; set; } = false;

        public virtual Appointment Appointment { get; set; } = null!;
        public virtual User Reviewee { get; set; } = null!;
        public virtual User Reviewer { get; set; } = null!;
    }
}
