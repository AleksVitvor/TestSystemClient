using DtoModels.Login;
using DtoModels.RequestModels.Login;
using System.Threading.Tasks;

namespace Services.Login.Login
{
    public interface ILoginService
    {
        public Task<RequestLoginModel> LogIn(LoginModelDto loginModel);
    }
}
