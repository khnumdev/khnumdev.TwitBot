namespace Khnumdev.TwitBot.Services
{
    using Core.TextAnalyzer.Model;
    using System.Threading.Tasks;

    public interface IMessageMatcherProcessor
    {
        Task<string> ProcessAsync(AnalysisResult result, string input);
    }
}
