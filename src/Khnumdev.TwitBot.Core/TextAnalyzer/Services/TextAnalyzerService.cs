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

    public class TextAnalyzerService: ITextAnalyzerService
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

        async Task<String> CallEndpoint(HttpClient client, string uri, byte[] byteData)
        {
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(uri, content);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}