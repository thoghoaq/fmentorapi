namespace FMentorAPI.DTOs.RequestModel.UpdateRequestModel
{
    public class UpdateBookingRequestModel
    {
        public string Status { get; set; } = null!;
        public string? ReasonForRejection { get; set; }
    }
}
