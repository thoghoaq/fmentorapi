using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class WalletResponseModel
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
