﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class Review
    {
        [Key]
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

        public virtual Appointment Appointment { get; set; } = null!;
        public virtual User Reviewee { get; set; } = null!;
        public virtual User Reviewer { get; set; } = null!;
    }
}
