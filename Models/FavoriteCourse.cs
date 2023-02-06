using System.ComponentModel.DataAnnotations.Schema;

namespace FMentorAPI.Models
{
    public class FavoriteCourse
    {
        [Column("mentee_id")]
        public int MenteeId { get; set; }
        [Column("course_id")]
        public int CourseId { get; set; }
        public virtual Mentee Mentee {get;set;}
        public virtual Course Course { get; set; }
    }
}
