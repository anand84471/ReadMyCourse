using Newtonsoft.Json;

namespace StudentDashboard.HttpResponse.Student
{
    public class StudentRegisterResponse
    {
        [JsonIgnore]
        public long UserId { get; set; }
        [JsonProperty("full_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("email_id")]
        public string EmailId { get; set; }
        [JsonProperty("token")]
        public string JwtToken { get; set; }
    }
}