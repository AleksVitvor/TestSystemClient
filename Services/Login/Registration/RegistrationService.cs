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
            try
            {
                var client = HttpClientFactory.CreateClient("notauthorized");
                registerModel.Password = Crypto.SHA256GetHash(registerModel.Password);
                var result = await client.PostAsJsonAsync("/user/register", registerModel);
                return result.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}