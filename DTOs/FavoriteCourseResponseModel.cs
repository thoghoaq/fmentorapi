namespace FMentorAPI.DTOs
{
    public class FavoriteCourseResponseModel
    {
        public int MenteeId { get; set; }
        public int CourseId { get; set; }
        public bool IsFavorite { get; set; }
    }
}
