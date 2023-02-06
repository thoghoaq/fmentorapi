using FMentorAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class UserResponseModel
    {
        public UserResponseModel()
        {
            Jobs = new HashSet<JobResponseModel>();
            Educations = new HashSet<EducationResponseModel>();
        }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }
        public string Description { get; set; } = null!;
        public string VideoIntroduction { get; set; } = null!;
        public byte IsMentor { get; set; }
        public string? Photo { get; set; }

        public virtual ICollection<JobResponseModel> Jobs { get; set; }
        public virtual ICollection<EducationResponseModel> Educations { get; set; }
    }
}
