using DtoModels.Login;
using Services.CryptoService;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login.Registration
{
    public class RegistrationService : IRegistrationService
    {
        IHttpClientFactory HttpClientFactory;
        ICrypto Crypto;

        public RegistrationService(IHttpClientFactory httpClientFactory, ICrypto crypto)
        {
            HttpClientFactory = httpClientFactory;
            Crypto = crypto;
        }
        public async Task<bool> Register(RegisterModelDto registerModel)
        {
            var client = HttpClientFactory.CreateClient();
            registerModel.Password = Crypto.SHA256GetHash(registerModel.Password);
            var result = await client.PutAsJsonAsync("https://courcestage.herokuapp.com/user", registerModel);
            return result.IsSuccessStatusCode;
        }
    }
}