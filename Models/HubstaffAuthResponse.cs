using Newtonsoft.Json;

namespace HubstaffReportGenerator.Models
{
    //[JsonObject("user")]
    public class HubstaffAuthResponse
    {
        [JsonProperty("user")]
        public UserResponse User { get; set; }
    }

    public class UserResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("last_activity")]
        public string LastActivity { get; set; }
        [JsonProperty("auth_token")]
        public string AuthToken { get; set; }
    }
}
