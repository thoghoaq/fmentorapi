using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.Models
{
    public partial class UserPermission
    {
        [Key]
        public byte IsMentor { get; set; }
        [Range(0, 1, ErrorMessage = "CanSeeSettings must be either 0 or 1")]
        public byte CanSeeSettings { get; set; }
        [Range(0, 1, ErrorMessage = "CanSeePolicy must be either 0 or 1")]
        public byte CanSeePolicy { get; set; }
        [Range(0, 1, ErrorMessage = "CanLogout must be either 0 or 1")]
        public byte CanLogout { get; set; }
        [Range(0, 1, ErrorMessage = "CanFollowMentors must be either 0 or 1")]
        public byte CanFollowMentors { get; set; }
        [Range(0, 1, ErrorMessage = "CanRequestToMentor must be either 0 or 1")]
        public byte CanRequestToMentor { get; set; }
        [Range(0, 1, ErrorMessage = "CanMakeSchedule must be either 0 or 1")]
        public byte CanMakeSchedule { get; set; }
        [Range(0, 1, ErrorMessage = "CanSeeCourses must be either 0 or 1")]
        public byte CanSeeCourses { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
