using DtoModels.Login;
using System.Threading.Tasks;

namespace Services.Login.Registration
{
    public interface IRegistrationService
    {
        public Task<bool> Register(RegisterModelDto registerModel);
    }
}
