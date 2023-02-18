using Newtonsoft.Json;

namespace FMentorAPI.BusinessLogic.DTOs
{
    public class NotificationResponseModel
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
