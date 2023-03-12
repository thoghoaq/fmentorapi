namespace FMentorAPI.BusinessLogic.DTOs
{
    public class BookingResponseModel
    {
        public int BookingId { get; set; }
        public int MentorId { get; set; }
        public int MenteeId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; } = null!;
        public string? ReasonForRejection { get; set; }
        public int Duration { get; set; }

        public virtual MentorResponseModel2 Mentor { get; set; } = null!;
        public virtual MenteeResponseModel2 Mentee { get; set; } = null!;
    }
}
