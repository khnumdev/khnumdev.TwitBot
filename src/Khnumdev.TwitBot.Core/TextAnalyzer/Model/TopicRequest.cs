namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class TopicRequest
    {
        [JsonProperty(PropertyName = "documents")]
        public List<Document> Documents { get; set; }

        [JsonProperty(PropertyName = "stopWords")]
        public string[] StopWords { get; set; }

        [JsonProperty(PropertyName = "stopPhrases")]
        public string [] StopPhrases { get; set; }
    }
}
