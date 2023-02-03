using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMentorAPI.DTOs
{
    public class UserPermissionResponseModel
    {
        public byte IsMentor { get; set; }
        public byte CanSeeSettings { get; set; }
        public byte CanSeePolicy { get; set; }
        public byte CanLogout { get; set; }
        public byte CanFollowMentors { get; set; }
        public byte CanRequestToMentor { get; set; }
        public byte CanMakeSchedule { get; set; }
        public byte CanSeeCourses { get; set; }
    }
}
