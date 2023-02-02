using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class Course
    {
        public Course()
        {
            Mentees = new HashSet<Mentee>();
        }

        public int CourseId { get; set; }
        public int MentorId { get; set; }
        public string Title { get; set; } = null!;
        public string Instructor { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Photo { get; set; } = null!;

        public virtual Mentor Mentor { get; set; } = null!;

        public virtual ICollection<Mentee> Mentees { get; set; }
    }
}
