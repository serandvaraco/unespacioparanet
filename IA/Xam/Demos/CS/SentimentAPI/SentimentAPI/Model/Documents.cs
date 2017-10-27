using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SentimentAPI.Model
{
    class SentimentModel
    {
        public IEnumerable<document> documents { get; set; }
    }
    class document
    {
        public decimal score { get; set; }
        public int id { get; set; }
    }

    class SentimentClient
    {
        HttpClient httpClient;
        string sentimentUri = "https://eastus2.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment?";
        public SentimentClient()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "abd85f4493d14a1eb8d8d4319c7c3ced");
        }

        public async Task<SentimentModel> GetSentimentAsync(string valueText)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(@"{
                'documents': [
                  {
                    'language': 'es',
                    'id': '1',
                    'text': '" + valueText + @"' 
                  }
                ]}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(sentimentUri, content);

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<SentimentModel>(result);

            }
        }
    }
}