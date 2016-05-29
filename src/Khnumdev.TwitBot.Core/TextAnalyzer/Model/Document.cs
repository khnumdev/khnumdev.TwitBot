namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;

    public class Document
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}