namespace Khnumdev.TwitBot.Services
{
    using Core.TextAnalyzer.Model;
    using System.Threading.Tasks;

    public interface IMessageMatcherProcessor
    {
        Task<MatchedMessage> ProcessAsync(string username, AnalysisResult result, string input);

        int CalculatePharseCoincidence(string keyPhrases, string tweetKeyPhrases);
    }
}
