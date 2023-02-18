using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.BusinessLogic.DTOs.RequestModel
{
    public class EducationRequestModel
    {
        [Required]
        public int UserId { get; set; }
        [Required(ErrorMessage = "School is required")]
        [StringLength(100, ErrorMessage = "School is mo more than 100 charactors.")]
        public string School { get; set; }
        [Required(ErrorMessage = "Major is required")]
        [StringLength(100, ErrorMessage = "Major is mo more than 100 charactors.")]
        public string Major { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; } = false;
    }
}
