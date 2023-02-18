namespace FMentorAPI.BusinessLogic.DTOs
{
    public class CourseResponseModel
    {
        public int CourseId { get; set; }
        public int MentorId { get; set; }
        public string Title { get; set; } = null!;
        public string Instructor { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Photo { get; set; } = null!;
    }
}
