using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMentorAPI.BusinessLogic.DTOs.RequestModel
{
    public class ReviewRequestModel
    {
        public int ReviewId { get; set; }
        public int AppointmentId { get; set; }
        public int ReviewerId { get; set; }
        public int RevieweeId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
