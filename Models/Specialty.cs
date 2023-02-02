﻿using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class Specialty
    {
        public Specialty()
        {
            UserSpecialties = new HashSet<UserSpecialty>();
        }

        public int SpecialtyId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<UserSpecialty> UserSpecialties { get; set; }
    }
}
