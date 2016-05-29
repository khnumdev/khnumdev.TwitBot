namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    using System.Collections.Generic;

    public class AnalysisResult
    {
        public float Sentiment { get; set; }

        public List<string> KeyPhrases { get; set; }

        public string OriginalText { get; set; }
    }
}