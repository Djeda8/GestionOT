using Newtonsoft.Json;

namespace DOU.GestionOT.App.MVVM.Models.Login
{
    public class RegisterResponse
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("detail")]
        public string    Detail { get; set; }

        [JsonProperty("instance")]
        public string? Instance { get; set; }

        [JsonProperty("errors")]
        public Errors Errors { get; set; }

        [JsonProperty("additionalProp1")]
        public string? AdditionalProp1 { get; set; }

        [JsonProperty("additionalProp2")]
        public string? AdditionalProp2 { get; set; }

        [JsonProperty("additionalProp3")]
        public string? AdditionalProp3 { get; set; }
    }
    public class Errors
    {
        public List<string>? AdditionalProp1 { get; set; }
        public List<string>? AdditionalProp2 { get; set; }
        public List<string>? AdditionalProp3 { get; set; }
    }

}
