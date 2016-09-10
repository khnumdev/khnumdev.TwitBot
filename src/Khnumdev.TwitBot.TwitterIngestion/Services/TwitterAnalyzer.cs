namespace Khnumdev.TwitBot.TwitterIngestion.Services
{
    using Core.TextAnalyzer.Services;
    using Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class TwitterAnalyzer : ITwitterAnalyzer
    {
        readonly ITextAnalyzerService _textAnalyzerService;
        readonly ITwitterRepository _twitterRepository;

        public TwitterAnalyzer(ITextAnalyzerService textAnalyzerService,
            ITwitterRepository twitterRepository)
        {
            _textAnalyzerService = textAnalyzerService;
            _twitterRepository = twitterRepository;
        }

        public async Task CheckPendingTweets()
        {
            var pendingTweets = await _twitterRepository.GetPendingTweetsToAnalyzeAsync();

            // Only call to Cognitive API if pending tweets are more than 50
            // This is to reduce the number of calls to the API
            if (pendingTweets.Count > 5)
            {
                var requestToAnalyze = pendingTweets
                    .Select(i => i.Text)
                    .ToList();

                var analysisResults = await _textAnalyzerService.AnalyzeAsync(requestToAnalyze);

                foreach (var analysisResult in analysisResults)
                {
                    var currentTweet = pendingTweets
                        .Where(i => i.Text == analysisResult.OriginalText)
                        .First();

                    currentTweet.Sentiment = analysisResult.Sentiment;
                    currentTweet.KeyPhrases = string.Join(",", analysisResult.KeyPhrases);
                }

                if (analysisResults.Any())
                {
                    await _twitterRepository.UpdateAsync(pendingTweets);
                }
            }
        }
    }
}
