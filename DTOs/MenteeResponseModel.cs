using FMentorAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class MenteeResponseModel
    {
        public int MenteeId { get; set; }
        public int UserId { get; set; }
        public virtual UserResponseModel User { get; set; } = null!;
    }
}
