using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs.RequestModel.UpdateRequestModel
{
    public class UpdateUserRequestModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [Range(0, int.MaxValue, ErrorMessage = "Age must be a positive number")]
        public int? Age { get; set; }
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string? Description { get; set; }
        [StringLength(255, ErrorMessage = "Video introduction link cannot be longer than 255 characters.")]
        public string? VideoIntroduction { get; set; }
        [StringLength(255, ErrorMessage = "Photo link cannot be longer than 255 characters.")]
        public string? Photo { get; set; }
    }
}
