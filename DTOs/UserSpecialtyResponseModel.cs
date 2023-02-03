using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class UserSpecialtyResponseModel
    {
        public int UserSpecialtyId { get; set; }
        public int UserId { get; set; }
        public int SpecialtyId { get; set; }
    }
}
