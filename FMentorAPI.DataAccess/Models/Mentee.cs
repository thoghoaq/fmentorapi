﻿namespace FMentorAPI.DataAccess.Models
{
    public partial class Mentee
    {
        public Mentee()
        {
            Appointments = new HashSet<Appointment>();
            Bookings = new HashSet<Booking>();

            Courses = new HashSet<Course>();
            FollowedMentors = new HashSet<FollowedMentor>();
            FavoriteCourses = new HashSet<FavoriteCourse>();

            Courses = new HashSet<Course>();
            Mentors = new HashSet<Mentor>();

        }

        public int MenteeId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }


        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<FollowedMentor> FollowedMentors {get;set;}
        public virtual ICollection<FavoriteCourse> FavoriteCourses { get; set; }

        public virtual ICollection<Mentor> Mentors { get; set; }

    }
}
