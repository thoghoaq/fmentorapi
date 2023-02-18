using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.BusinessLogic.DTOs.RequestModel
{
    public class JobRequestModel
    {
        [Required]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Company is required")]
        [StringLength(100, ErrorMessage = "Company is mo more than 100 charactors.")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Role is required")]
        [StringLength(100, ErrorMessage = "Role is mo more than 100 charactors.")]
        public string Role { get; set; }
        [Required(ErrorMessage ="Start date is required")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; } = false;
    }
}
