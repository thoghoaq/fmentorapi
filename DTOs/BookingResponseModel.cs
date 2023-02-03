using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class BookingResponseModel
    {
        public int BookingId { get; set; }
        public int MentorId { get; set; }
        public int MenteeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; } = null!;
        public string? ReasonForRejection { get; set; }
    }
}
