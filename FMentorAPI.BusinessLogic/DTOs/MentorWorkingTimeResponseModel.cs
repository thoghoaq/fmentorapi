namespace FMentorAPI.BusinessLogic.DTOs
{
    public class MentorWorkingTimeResponseModel
    {
        public int MentorId { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
