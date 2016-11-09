namespace Khnumdev.TwitBot.Core.TextAnalyzer.Services
{
    using Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITextAnalyzerService
    {
        Task<AnalysisResult> AnalyzeAsync(string input);

        Task<List<AnalysisResult>> AnalyzeAsync(List<string> input);

        Task<List<TopicAnalysisResult>> AnalyzeTopicsAsync(List<string> input);
    }
}
