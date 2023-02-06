using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class EducationResponseModel
    {
        public int EducationId { get; set; }
        public int UserId { get; set; }
        public string School { get; set; } = null!;
        public string Major { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte IsCurrent { get; set; }
    }
}
