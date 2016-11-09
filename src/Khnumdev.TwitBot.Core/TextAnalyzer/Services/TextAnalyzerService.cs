namespace Khnumdev.TwitBot.Core.TextAnalyzer.Services
{
    using Model;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public class TextAnalyzerService : ITextAnalyzerService
    {
        const string BaseUrl = "https://westus.api.cognitive.microsoft.com/";

        const int NumLanguages = 1;

        readonly string _accountKey;

        public TextAnalyzerService()
        {
            _accountKey = ConfigurationManager.AppSettings["TextAnalytics"];
        }

        public async Task<AnalysisResult> AnalyzeAsync(string input)
        {
            var result = await AnalyzeAsync(new List<string>() { input });

            return result.FirstOrDefault();
        }

        public async Task<List<AnalysisResult>> AnalyzeAsync(List<string> input)
        {
            var result = new List<AnalysisResult>();

            using (var client = new HttpClient())
            {
                string response = default(string);

                client.BaseAddress = new Uri(BaseUrl);

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _accountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Document document = null;
                var request = new TextRequest() { Documents = new List<Document>() };
                for (int i = 1; i <= input.Count; i++)
                {
                    document = new Document
                    {
                        Id = i.ToString(),
                        Text = input[i - 1]
                    };

                    request.Documents.Add(document);
                }

                var serializedEntity = JsonConvert.SerializeObject(request);

                byte[] byteData = Encoding.UTF8.GetBytes(serializedEntity);

                // Detect key phrases:
                var uri = "text/analytics/v2.0/keyPhrases";
                response = await CallEndpoint(client, uri, byteData);
                var keyPhrases = JsonConvert.DeserializeObject<TextResponse<KeyPhraseResponse>>(response);

                // Detect sentiment:
                uri = "text/analytics/v2.0/sentiment";
                response = await CallEndpoint(client, uri, byteData);
                var sentiment = JsonConvert.DeserializeObject<TextResponse<SentimentResponse>>(response);

                result = request.Documents
                    .Select(i =>
                    {
                        var item = new AnalysisResult
                        {
                            Sentiment = sentiment.Documents.Single(j => j.Id == i.Id).Score,
                            KeyPhrases = keyPhrases.Documents.Single(j => j.Id == i.Id).KeyPhrases,
                            OriginalText = i.Text
                        };

                        return item;
                    })
                        .ToList();
            }

            return result;
        }

        public async Task<List<TopicAnalysisResult>> AnalyzeTopicsAsync(List<string> input)
        {
            var result = new List<TopicAnalysisResult>();

            using (var client = new HttpClient())
            {
                string response = default(string);

                client.BaseAddress = new Uri(BaseUrl);

                // Request headers.
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _accountKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Document document = null;
                var request = new TopicRequest() { Documents = new List<Document>() };
                for (int i = 1; i <= input.Count; i++)
                {
                    document = new Document
                    {
                        Id = i.ToString(),
                        Text = input[i - 1]
                    };

                    request.Documents.Add(document);
                }

                var serializedEntity = JsonConvert.SerializeObject(request);

                byte[] byteData = Encoding.UTF8.GetBytes(serializedEntity);

                // Topics
                var uri = "text/analytics/v2.0/topics";
                var operationUri = "text/analytics/v2.0/operations/{0}";

                response = await CallEndpoint(client, uri, byteData, operationUri);

                var topicResponse = JsonConvert.DeserializeObject<TopicResponse>(response);

                var indexedTopics = topicResponse
                    .Topics
                    .ToDictionary(i => i.Id, i => i);

                result = request.Documents
                    .Select(i =>
                    {
                        var item = new TopicAnalysisResult
                        {
                            Distance = topicResponse.TopicAssignments.Single(j => j.DocumentId == i.Id).Distance,
                            KeyPhrase = indexedTopics[topicResponse.TopicAssignments.Single(j => j.DocumentId == i.Id).TopicId].KeyPhrase,
                            OriginalText = i.Text
                        };

                        return item;
                    })
                        .ToList();
            }

            return result;
        }

        async Task<string> CallEndpoint(HttpClient client, string uri, byte[] byteData, string operationUrl = null)
        {
            var result = string.Empty;

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(uri, content);

                if (response.Headers.Contains("operation-location"))
                {
                    var operationId = response.Headers.GetValues("operation-location").Single();
                    var operationRequest = string.Format(operationUrl, operationId);

                    do
                    {
                        // Request every minute
                        await Task.Delay(60000);

                        response = await client.GetAsync(operationRequest);
                        result = await response.Content.ReadAsStringAsync();
                    } while (result.Contains("\"status\": \"succeded\""));
                }
                else
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }

            return result;
        }
    }
}