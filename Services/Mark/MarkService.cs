using DtoModels.Answers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services.Mark
{
    public class MarkService : IMarkService
    {
        IHttpClientFactory HttpClientFactory;
        HttpClient client;

        public MarkService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            client = HttpClientFactory.CreateClient("authorized");
        }
        public async Task<int> GetMarkFromAnswers(TestAnswer answer, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.PostAsJsonAsync("/answer/check", answer);
                return await result.Content.ReadFromJsonAsync<int>();
            }
            catch
            {
                return -1;
            }
        }
    }
}
