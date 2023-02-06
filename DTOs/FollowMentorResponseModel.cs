namespace FMentorAPI.DTOs
{
    public class FollowMentorResponseModel
    {
        public int MentorId { get; set; }
        public int MenteeId { get; set; }
        public bool IsFollow { get; set; }
    }
}
