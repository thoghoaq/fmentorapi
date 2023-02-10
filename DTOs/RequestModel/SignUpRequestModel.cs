using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs.RequestModel
{
    public class SignUpRequestModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(18,MinimumLength = 8, ErrorMessage = "Password is between 8 and 16 charactor.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(18, MinimumLength = 8, ErrorMessage = "Confirm Password is between 8 and 16 charactor.")]
        public string ConfirmPassword { get; set; }
    }
}
