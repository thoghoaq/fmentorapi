using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class Mentor
    {
        public Mentor()
        {
            Bookings = new HashSet<Booking>();
            AppointmentMentors = new HashSet<Appointment>();
            Courses = new HashSet<Course>();
            MentorWorkingTimes = new HashSet<MentorWorkingTime>();
        }
        [Key]
        public int MentorId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Specialty cannot be longer than 100 characters.")]
        public string Specialty { get; set; } = null!;
        [Required]
        [Range(0, 999.99, ErrorMessage = "Hourly rate must be between 0 and 999.99.")]
        public decimal HourlyRate { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Availability must be between 0 and 100.")]
        public byte Availability { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Appointment> AppointmentMentors { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<MentorWorkingTime> MentorWorkingTimes { get; set; }
    }
}
