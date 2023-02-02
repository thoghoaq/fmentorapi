using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class Education
    {
        [Key]
        public int EducationId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "School name cannot be longer than 100 characters.")]
        public string School { get; set; } = null!;
        [Required]
        [StringLength(100, ErrorMessage = "Major cannot be longer than 100 characters.")]
        public string Major { get; set; } = null!;
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
