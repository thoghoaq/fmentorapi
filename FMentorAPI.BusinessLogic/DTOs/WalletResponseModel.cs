using FMentorAPI.DataAccess.Models;

namespace FMentorAPI.BusinessLogic.DTOs
{
    public class WalletResponseModel
    {
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
