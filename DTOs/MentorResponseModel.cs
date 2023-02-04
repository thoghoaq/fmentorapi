using FMentorAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public partial class MentorResponseModel
    {
        public int MentorId { get; set; }
        public int UserId { get; set; }
        public string Specialty { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public byte Availability { get; set; }
        public virtual UserResponseModel User { get; set; } = null!;
    }
}
