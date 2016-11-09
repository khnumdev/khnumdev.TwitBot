namespace Khnumdev.TwitBot.SyncJob.Processors
{
    using Data.DWH.Helpers;
    using Data.DWH.Model.Dimensions;
    using Data.DWH.Model.Facts;
    using Data.DWH.Repositories;
    using Data.Repositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class TopicProcessor : ITopicProcessor
    {
        readonly ITwitterRepository _twitterRepository;
        readonly IDWHRepository _dwhRepository;

        public TopicProcessor(ITwitterRepository twitterRepository, IDWHRepository dwhRepository)
        {
            _twitterRepository = twitterRepository;
            _dwhRepository = dwhRepository;
        }

        public async Task ProcessAsync()
        {
            var trendingTopicsToAdd = await _twitterRepository.GetNewTrendingTopics();

            await ProcessDimensions(trendingTopicsToAdd);
            await ProcessFacts(trendingTopicsToAdd);
        }

        async Task ProcessDimensions(List<Data.Model.TrendingTopic> trendingTopicsToAdd)
        {
            var trendingTopics = trendingTopicsToAdd
                .Select(i => new TrendingTopic
                {
                    IsPromoted = i.IsPromoted,
                    Name = Sanitize(i.Text)
                })
                .ToList();

            var geographies = trendingTopicsToAdd
                .Select(i => i.Country)
                .Distinct()
                .Select(i => new Geography
                {
                    Name = i
                }).ToList();

            await _dwhRepository.UpsertAsync(geographies);
            await _dwhRepository.UpsertAsync(trendingTopics);
        }

        async Task ProcessFacts(List<Data.Model.TrendingTopic> trendingTopicsToAdd)
        {
            var trendingTopics = await _dwhRepository.GetAllAsync<TrendingTopic>();
            var geographies = await _dwhRepository.GetAllAsync<Geography>();

            var indexedTopics = trendingTopics.ToDictionary(i => i.Name, i => i);
            var indexedGeographies = geographies.ToDictionary(i => i.Name, i => i);

            foreach (var trendingTopic in trendingTopicsToAdd)
            {
                var topic = new Topic
                {
                    DateId = DateHelper.GetKeyFromDate(trendingTopic.Date),
                    GeographyId = indexedGeographies[trendingTopic.Country].Id,
                    TrendingTopicId = indexedTopics[Sanitize(trendingTopic.Text)].Id
                };

                await _dwhRepository.AddFactAsync(topic);
            }
        }

        string Sanitize(string trendingTopicText)
        {
            return trendingTopicText.Replace("#", string.Empty);
        }
    }
}
