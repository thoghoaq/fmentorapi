using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace FMentorAPI.Models
{
    public class FollowedMentor
    {
        [Column("mentor_id")]
        public int MentorId { get; set; }
        [Column("mentee_id")]
        public int MenteeId { get; set; }
        public virtual Mentee Mentee { get; set; }
        public virtual Mentor Mentor { get; set; }
    }
}
