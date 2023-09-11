using Profiles.DAL.Entities;

namespace Profiles.BLL.Services
{
    public interface ISignInManager
    {
        string SignIn(User user);
    }
}