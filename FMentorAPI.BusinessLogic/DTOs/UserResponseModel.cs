namespace FMentorAPI.BusinessLogic.DTOs
{
    public class UserResponseModel
    {
        public UserResponseModel()
        {
            Educations = new HashSet<EducationResponseModel>();
            Jobs = new HashSet<JobResponseModel>();
            Mentees = new HashSet<MenteeResponseModel>();
            Mentors = new HashSet<MentorResponseModel>();
            Payments = new HashSet<PaymentResponseModel>();
            ReviewReviewees = new HashSet<ReviewResponseModel>();
            ReviewReviewers = new HashSet<ReviewResponseModel>();
            UserSpecialties = new HashSet<UserSpecialtyResponseModel>();
        }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }
        public string Description { get; set; } = null!;
        public string VideoIntroduction { get; set; } = null!;
        public byte IsMentor { get; set; }
        public string? Photo { get; set; }
        public string AccessToken { get; set; }

        public virtual UserPermissionResponseModel IsMentorNavigation { get; set; } = null!;
        public virtual WalletResponseModel? Wallet { get; set; }
        public virtual ICollection<EducationResponseModel> Educations { get; set; }
        public virtual ICollection<JobResponseModel> Jobs { get; set; }
        public virtual ICollection<MenteeResponseModel> Mentees { get; set; }
        public virtual ICollection<MentorResponseModel> Mentors { get; set; }
        public virtual ICollection<PaymentResponseModel> Payments { get; set; }
        public virtual ICollection<ReviewResponseModel> ReviewReviewees { get; set; }
        public virtual ICollection<ReviewResponseModel> ReviewReviewers { get; set; }
        public virtual ICollection<UserSpecialtyResponseModel> UserSpecialties { get; set; }
    }
}
