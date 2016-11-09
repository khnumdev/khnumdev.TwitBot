namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class TopicResponse
    {
        [JsonProperty(PropertyName = "topics")]
        public List<Topic> Topics { get; set; }

        [JsonProperty(PropertyName = "topicAssignments")]
        public List<TopicAssignment> TopicAssignments { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public List<ErrorResponse> Errors { get; set; }
    }
}
