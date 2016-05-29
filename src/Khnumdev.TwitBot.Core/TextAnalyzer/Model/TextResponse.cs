namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class TextResponse<T> where T : class
    {
        [JsonProperty(PropertyName = "documents")]
        public List<T> Documents { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public List<ErrorResponse> Errors { get; set; }
    }
}