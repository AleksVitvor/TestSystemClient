using DtoModels.User;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services.User
{
    public class UserService : IUserService
    {
        IHttpClientFactory HttpClientFactory;
        HttpClient client;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            client = HttpClientFactory.CreateClient("authorized");
        }

        public async Task<bool> ChangeUserRole(UserModelDto user, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutAsJsonAsync("/user", user);
            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<UserRequestModel>> GetUsers(string token)
        {
            IEnumerable<UserRequestModel> users;

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync("/user");
                users = await result.Content.ReadFromJsonAsync<IEnumerable<UserRequestModel>>();
            }
            catch
            {
                users = null;
            }

            return users;
        }

        public async Task<string> GetUserName(string token)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync("/user/one");
                return await result.Content.ReadFromJsonAsync<string>();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
