namespace FMentorAPI.BusinessLogic.DTOs
{
    public class MentorAvailabilityResponseModel
    {
        public int MentorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
