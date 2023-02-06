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

        public int SpecialtyId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Specialty cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
        [StringLength(255, ErrorMessage = "Picture cannot be longer than 255 characters.")]
        public string? Picture { get; set; }


        public virtual ICollection<UserSpecialty> UserSpecialties { get; set; }
    }
}
