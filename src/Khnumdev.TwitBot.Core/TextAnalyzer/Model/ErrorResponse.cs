namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;

    public class ErrorResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}