using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class ReviewResponseModel
    {
        public int ReviewId { get; set; }
        public int AppointmentId { get; set; }
        public int ReviewerId { get; set; }
        public int RevieweeId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
        
    }
}
