using System;
using System.Collections.Generic;

namespace FMentorAPI.Models
{
    public partial class UserPermission
    {
        public int UserId { get; set; }
        public byte CanSeeSettings { get; set; }
        public byte CanSeePolicy { get; set; }
        public byte CanLogout { get; set; }
        public byte CanFollowMentors { get; set; }
        public byte CanRequestToMentor { get; set; }
        public byte CanMakeSchedule { get; set; }
        public byte CanSeeCourses { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
