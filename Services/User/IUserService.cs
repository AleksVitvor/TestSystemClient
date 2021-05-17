using DtoModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.User
{
    public interface IUserService
    {
        public Task<IEnumerable<UserRequestModel>> GetUsers(string token);

        public Task<bool> ChangeUserRole(UserModelDto user, string token);

        public Task<string> GetUserName(string token);
    }
}
