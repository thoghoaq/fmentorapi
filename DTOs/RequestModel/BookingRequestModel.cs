namespace FMentorAPI.DTOs.RequestModel
{
    public class BookingRequestModel
    {
        public int MentorId { get; set; }
        public int MenteeId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; } = null!;
        public int Duration { get; set; }
    }
}
