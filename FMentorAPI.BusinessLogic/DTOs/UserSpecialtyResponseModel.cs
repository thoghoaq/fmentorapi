namespace FMentorAPI.BusinessLogic.DTOs
{
    public class UserSpecialtyResponseModel
    {
        public int UserSpecialtyId { get; set; }
        public int UserId { get; set; }
        public int SpecialtyId { get; set; }
        public UserInfo User { get; set; }
    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }
        public string Description { get; set; } = null!;
        public string VideoIntroduction { get; set; } = null!;
        public byte IsMentor { get; set; }
        public string? Photo { get; set; }
    }
}
