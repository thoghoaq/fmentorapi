using System.ComponentModel.DataAnnotations.Schema;

namespace FMentorAPI.DataAccess.Models
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
