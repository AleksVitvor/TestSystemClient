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
                    .Where(x => x.Question != string.Empty)
                    .Select(x => new Question()
                    {
                        QuestionId = x.QuestionId,
                        QuestionString = x.Question
                    })
                    .AsEnumerable();
                questions.TestId = questionsWithTests.FirstOrDefault().TestId;
            }
            catch
            {
                questions = null;
            }

            return questions;
        }

        public async Task<bool> CreateQuestion(QuestionAddModel question, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.PostAsJsonAsync("/question", question);
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteQuestion(int questionId, string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.DeleteAsync($"/question?id={questionId}");
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<QuestionWithAnswersDto>> GetQuestionWithAnswers(int testId, string token)
        {
            IEnumerable<QuestionWithAnswersDto> questionWithAnswers;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync($"/question/list?testId={testId}");
                questionWithAnswers = await result.Content.ReadFromJsonAsync<IEnumerable<QuestionWithAnswersDto>>();
            }
            catch
            {
                questionWithAnswers = null;
            }
            return questionWithAnswers;
        }
    }
}
