using DtoModels.Login;
using DtoModels.RequestModels.Login;
using Services.CryptoService;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Services.Login.Login
{
    public class LoginService : ILoginService
    {
        IHttpClientFactory HttpClientFactory;
        ICrypto Crypto;

        public LoginService(IHttpClientFactory httpClientFactory, ICrypto crypto)
        {
            HttpClientFactory = httpClientFactory;
            Crypto = crypto;
        }
        public async Task<RequestLoginModel> LogIn(LoginModelDto loginModel)
        {
            RequestLoginModel requestLogin;

            try
            {
                var client = HttpClientFactory.CreateClient("notauthorized");
                loginModel.Password = Crypto.SHA256GetHash(loginModel.Password);
                var result = await client.PostAsJsonAsync("/user/login", loginModel);
                requestLogin = await result.Content.ReadFromJsonAsync<RequestLoginModel>();
            }
            catch (Exception)
            {
                requestLogin = null;
            }

            return requestLogin;
        }
    }
}
