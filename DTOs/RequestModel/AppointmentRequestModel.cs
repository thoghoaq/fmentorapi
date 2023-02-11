namespace FMentorAPI.DTOs.RequestModel
{
    public class AppointmentRequestModel
    {
        public int MentorId { get; set; }
        public int MenteeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string GoogleMeetLink { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Duration { get; set; }
        public string Status { get; set; } = null!;
        public string? Note { get; set; }
        public bool IsReviewed { get; set; }
    }
}
