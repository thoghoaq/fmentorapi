namespace FMentorAPI.BusinessLogic.DTOs
{
    public class JobResponseModel
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string Company { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte IsCurrent { get; set; }
    }
}
