using DtoModels.RequestModels.Test;
using DtoModels.Test;
using DtoModels.User;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services.TestService.Tests
{
    public class TestService : ITestService
    {
        IHttpClientFactory HttpClientFactory;

        public TestService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public async Task<bool> CreateTest(TestModelDto test, string token)
        {
            var client = HttpClientFactory.CreateClient("authorized");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostAsJsonAsync("/tests", test);
            return result.IsSuccessStatusCode;
        }

        public async Task<TestRequestedModel> GetOneTest(int id, string token)
        {
            TestRequestedModel requestTest;

            try
            {
                var client = HttpClientFactory.CreateClient("authorized");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync($"/tests/test?id={id}");
                requestTest = await result.Content.ReadFromJsonAsync<TestRequestedModel>();
            }
            catch (Exception)
            {
                requestTest = null;
            }

            return requestTest;
        }

        public async Task<IEnumerable<TestRequestedModel>> GetTests(string token)
        {
            IEnumerable<TestRequestedModel> tests;

            try
            {
                var client = HttpClientFactory.CreateClient("authorized");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync("/tests");
                tests = await result.Content.ReadFromJsonAsync<IEnumerable<TestRequestedModel>>();
            }
            catch (Exception e)
            {
                tests = null;
            }

            return tests;
        }

        public async Task<bool> UpdateTest(TestRequestedModel test, string token)
        {
            var client = HttpClientFactory.CreateClient("authorized");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutAsJsonAsync("/tests", test);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeTestActivity(TestRequestedModel test, string token)
        {
            var client = HttpClientFactory.CreateClient("authorized");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutAsJsonAsync($"/tests/test", test);
            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<StudentDto>> ReadStudentsForAssign(int testId, string token)
        {
            IEnumerable<StudentDto> students;

            try
            {
                var client = HttpClientFactory.CreateClient("authorized");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync($"/assign?testId={testId}");
                students = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDto>>();
            }
            catch
            {
                students = null;
            }

            return students;
        }

        public async Task<StudentDto> UpdateStudentsAssign(StudentDto student, int testId, string token)
        {
            try
            {
                var client = HttpClientFactory.CreateClient("authorized");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage result;

                var model = new TestWithUser()
                {
                    TestId = testId,
                    UserId = student.UserId
                };

                if(student.IsAssigned)
                {
                    result = await client.PostAsJsonAsync("/assign", model);
                }
                else
                {
                    result = await client.DeleteAsync($"/assign?testId={testId}&userId={student.UserId}");
                }

                if (result.IsSuccessStatusCode)
                    return student;
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
