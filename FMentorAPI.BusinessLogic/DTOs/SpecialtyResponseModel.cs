namespace FMentorAPI.BusinessLogic.DTOs
{
    public class SpecialtyResponseModel
    {
        public int SpecialtyId { get; set; }
        public string Name { get; set; } = null!;
        public string? Picture { get; set; }
        public int NumberMentor { get; set; }


    }
}
