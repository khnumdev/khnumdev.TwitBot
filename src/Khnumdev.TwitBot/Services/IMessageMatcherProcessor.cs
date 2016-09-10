namespace Khnumdev.TwitBot.Services
{
    using Core.TextAnalyzer.Model;
    using System.Threading.Tasks;

    public interface IMessageMatcherProcessor
    {
        Task<MatchedMessage> ProcessAsync(AnalysisResult result, string input);
    }
}
