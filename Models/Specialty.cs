using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            UserSpecialties = new HashSet<UserSpecialty>();
        }
        [Key]
        public int SpecialtyId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Specialty cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;

        public virtual ICollection<UserSpecialty> UserSpecialties { get; set; }
    }
}
