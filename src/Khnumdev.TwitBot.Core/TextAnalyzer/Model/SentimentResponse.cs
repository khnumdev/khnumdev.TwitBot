namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;

    public class SentimentResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "score")]
        public float Score { get; set; }
    }
}