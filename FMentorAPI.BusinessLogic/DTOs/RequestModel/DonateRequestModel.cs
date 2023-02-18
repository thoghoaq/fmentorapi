using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.BusinessLogic.DTOs.RequestModel;

public class DonateRequestModel
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    [Range(1, 99999999)] public decimal Amount { get; set; }
    public string? Description { get; set; }
}