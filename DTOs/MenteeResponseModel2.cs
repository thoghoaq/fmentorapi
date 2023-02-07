namespace FMentorAPI.DTOs
{
    public class MenteeResponseModel2
    {
        public int MenteeId { get; set; }
        public int UserId { get; set; }
        public virtual UserResponseModel User { get; set; } = null!;
    }
}
