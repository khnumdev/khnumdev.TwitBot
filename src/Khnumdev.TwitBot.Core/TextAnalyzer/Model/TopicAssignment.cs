namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;

    public class TopicAssignment
    {
        [JsonProperty(PropertyName = "topicId")]
        public string TopicId { get; set; }

        [JsonProperty(PropertyName = "documentId")]
        public string DocumentId { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public float Distance { get; set; }
    }
}
