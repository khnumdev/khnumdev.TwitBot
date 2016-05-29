namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class KeyPhraseResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "keyPhrases")]
        public List<string> KeyPhrases { get; set; }
    }
}