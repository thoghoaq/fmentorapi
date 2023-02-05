using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class AppointmentResponseModel
    {
        public int AppointmentId { get; set; }
        public int MentorId { get; set; }
        public int MenteeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string GoogleMeetLink { get; set; } = null!;
        public int Duration { get; set; }
        public string Status { get; set; } = null!;
        public string? Note { get; set; }

        public virtual MentorResponseModel Mentor { get; set; } = null!;
    }
}
