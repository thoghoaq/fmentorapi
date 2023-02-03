using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class UserResponseModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public byte IsMentor { get; set; }
        public int Age { get; set; }
        public string Description { get; set; } = null!;
        public string VideoIntroduction { get; set; } = null!;
    }
}
