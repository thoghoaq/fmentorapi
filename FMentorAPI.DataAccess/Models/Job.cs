﻿using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DataAccess.Models
{
    public partial class Job
    {
        [Key]
        public int JobId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Company name cannot be longer than 100 characters.")]
        public string Company { get; set; } = null!;
        [Required]
        [StringLength(100, ErrorMessage = "Role name cannot be longer than 100 characters.")]
        public string Role { get; set; } = null!;
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public byte IsCurrent { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
