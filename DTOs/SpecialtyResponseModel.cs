using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class SpecialtyResponseModel
    {
        public int SpecialtyId { get; set; }
        public string Name { get; set; } = null!;
        public string? Picture { get; set; }
        public int NumberMentor { get; set; }

    }
}
