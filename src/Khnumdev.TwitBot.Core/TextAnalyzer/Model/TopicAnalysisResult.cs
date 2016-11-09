namespace Khnumdev.TwitBot.Core.TextAnalyzer.Model
{
    public class TopicAnalysisResult
    {
        public string KeyPhrase { get; set; }

        public float Distance { get; set; }

        public string OriginalText { get; set; }
    }
}
