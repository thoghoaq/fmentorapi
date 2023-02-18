using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using FMentorAPI.BusinessLogic.DTOs;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FMentorAPI.WebAPI.Extensions.ZoomAPI
{
    public interface IZoomExtension
    {
        Task<ZoomLinkModel> CreateMeetingAsync();
    }
    public class ZoomExtension : IZoomExtension
    {
        // Enter your API key and your API secret
        const string API_KEY = "NARTX0pATZa_tnEnyMYSDA";
        const string API_SEC = "xyQ5wbyL8VMEqR7B5Cm26ZPftGvby1nQdoOD";

        // create a function to generate a token
        // using the JWT library
        private string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(API_SEC);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim("iss", API_KEY)
                }),
                Expires = DateTime.UtcNow.AddSeconds(5000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<ZoomLinkModel> CreateMeetingAsync()
        {
            using (var client = new HttpClient())
            {
                // Create JSON data for POST requests
                var meetingDetails = new
                {
                    topic = "The title of your zoom meeting",
                    type = 2,
                    start_time = DateTime.Parse("2019-06-14T10:21:57"),
                    duration = "45",
                    timezone = "Europe/Madrid",
                    agenda = "test",
                    recurrence = new
                    {
                        type = 1,
                        repeat_interval = 1
                    },
                    settings = new
                    {
                        host_video = "true",
                        participant_video = "true",
                        join_before_host = "true",
                        mute_upon_entry = "False",
                        watermark = "true",
                        audio = "voip",
                        auto_recording = "cloud"
                    }
                };

                // Send a request with headers including a token and meeting details
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateToken());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(meetingDetails), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://api.zoom.us/v2/users/me/meetings", content);
                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine("\nCreating zoom meeting ...\n");

                // Converting the output into JSON and extracting the details
                dynamic result = JsonConvert.DeserializeObject(responseString);
                string joinURL = result.join_url;
                string meetingPassword = result.password;
                ZoomLinkModel zoomLinkModel = new ZoomLinkModel
                {
                    Url = joinURL,
                    Password = meetingPassword,
                };
                Console.WriteLine($"\nHere is your zoom meeting link {joinURL} and your password: \"{meetingPassword}\"\n");
                return zoomLinkModel;
            }
        }
    }
}
