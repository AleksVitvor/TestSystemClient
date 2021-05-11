using DtoModels.Answers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services.Answer
{
    public class AnswerService : IAnswerService
    {
        IHttpClientFactory HttpClientFactory;
        HttpClient client;

        public AnswerService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            client = HttpClientFactory.CreateClient("authorized");
        }

        public async Task<IList<AnswerDto>> GetAnswers(int questionId, string token)
        {
            IList<AnswerDto> answers = new List<AnswerDto>();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var receivedAnswers = await client.GetAsync($"/answer?questionId={questionId}");
                if (receivedAnswers.StatusCode == HttpStatusCode.NotFound)
                    return answers;
                answers = await receivedAnswers.Content.ReadFromJsonAsync<IList<AnswerDto>>();
            }
            catch
            {
                return answers = null;
            }

            return answers;
        }

        public async Task<AnswerDto> UpdateAnswer(AnswerDto answer, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.PutAsJsonAsync("/answer", answer);
                return await result.Content.ReadFromJsonAsync<AnswerDto>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<AnswerDto> CreateAnswer(AnswerDto answer, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.PostAsJsonAsync("/answer", answer);
                return await result.Content.ReadFromJsonAsync<AnswerDto>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAnswer(AnswerDto answer, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.DeleteAsync($"/answer?answerId={answer.AnswerId}");
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
