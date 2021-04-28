using DtoModels.RequestModels.Test;
using DtoModels.Test;
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
            var result = await client.PostAsJsonAsync("/test", test);
            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<TestRequestedModel>> GetTests(string token)
        {
            IEnumerable<TestRequestedModel> tests;

            try
            {
                var client = HttpClientFactory.CreateClient("authorized");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync("/test");
                tests = await result.Content.ReadFromJsonAsync<IEnumerable<TestRequestedModel>>();
            }
            catch (Exception e)
            {
                tests = null;
            }

            return tests;
        }
    }
}
