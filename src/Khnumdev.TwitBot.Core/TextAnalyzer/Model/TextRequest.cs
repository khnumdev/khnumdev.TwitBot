namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class TextRequest
    {
        [JsonProperty(PropertyName = "documents")]
        public List<Document> Documents { get; set; }
    }
}