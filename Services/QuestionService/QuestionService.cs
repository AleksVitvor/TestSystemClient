using DtoModels.Question;
using DtoModels.RequestModels.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        IHttpClientFactory HttpClientFactory;
        HttpClient client;

        public QuestionService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            client = HttpClientFactory.CreateClient("authorized");
        }

        public async Task<QuestionModelDto> GetQuestions(int testId, string token)
        {
            QuestionModelDto questions = new QuestionModelDto();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync($"/question?testId={testId}");
                var questionsWithTests = await result.Content.ReadFromJsonAsync<IEnumerable<QuestionRequestModel>>();
                questions.Test = questionsWithTests.FirstOrDefault().Test;
                questions.Questions = questionsWithTests
                    .Where(x => x.Question != String.Empty)
                    .Select(x => x.Question)
                    .AsEnumerable();
                questions.TestId = questionsWithTests.FirstOrDefault().TestId;
            }
            catch
            {
                questions = null;
            }

            return questions;
        }
    }
}
