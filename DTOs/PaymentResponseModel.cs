namespace FMentorAPI.DTOs
{
    public class PaymentResponseModel
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Note { get; set; }
    }
}
