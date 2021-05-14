using DtoModels.Answers;
using DtoModels.Mark;
using System.Collections.Generic;
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

        public async Task<IEnumerable<MarkDto>> GetMarkForTest(int testId, string token)
        {
            IEnumerable<MarkDto> marks;

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync($"/mark?testId={testId}");
                marks = await result.Content.ReadFromJsonAsync<IEnumerable<MarkDto>>();
            }
            catch
            {
                marks = null;
            }
            return marks;
        }

        public async Task<IEnumerable<MyMarkDto>> GetMarkForUser(string token)
        {
            IEnumerable<MyMarkDto> marks;

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync($"/mark/my");
                marks = await result.Content.ReadFromJsonAsync<IEnumerable<MyMarkDto>>();
            }
            catch
            {
                marks = null;
            }
            return marks;
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
