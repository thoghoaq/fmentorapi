namespace FMentorAPI.BusinessLogic.DTOs
{
    public partial class MentorResponseModel
    {
        public int MentorId { get; set; }
        public int UserId { get; set; }
        public string Specialty { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public byte Availability { get; set; }
        public int NumberFollower { get; set; }
        public int NumberMentee { get; set; }
        //public virtual UserResponseModel? User { get; set; } = null!;
    }
}
