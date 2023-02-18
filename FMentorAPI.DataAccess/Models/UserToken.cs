using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMentorAPI.DataAccess.Models
{
    [Table("UserToken")]
    public class UserToken
    {
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("token")]
        [StringLength(200, ErrorMessage = "Max length is 200")]
        [Required(ErrorMessage = "Token is required!")]
        public string Token { get; set; }
    }
}
