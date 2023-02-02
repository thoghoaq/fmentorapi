using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class UserSpecialty
    {
        public int UserSpecialtyId { get; set; }
        public int UserId { get; set; }
        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
