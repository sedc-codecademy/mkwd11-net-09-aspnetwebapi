using Profiles.BLL.Models;

namespace Profiles.BLL.Services
{
    public interface IUserService
    {
        string Login(UserLoginModel model);
        void Register(UserModel model);
    }
}