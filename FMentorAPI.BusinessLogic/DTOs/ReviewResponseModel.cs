namespace FMentorAPI.BusinessLogic.DTOs
{
    public class ReviewResponseModel
    {
        public int ReviewId { get; set; }
        public int AppointmentId { get; set; }
        public int ReviewerId { get; set; }
        public int RevieweeId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;

        public ReviewerInfo Reviewer { get; set; }
        
    }

    public class ReviewerInfo
    {
        public string Name { get; set; }
        public string? Photo  { get; set; }
    }
}
