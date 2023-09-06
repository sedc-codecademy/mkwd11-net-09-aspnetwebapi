using SEDC.MoviesApp.Dtos.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterDto registerDto);
        string LoginUser(LoginDto loginDto);
        UserDto Authenticate(LoginDto loginDto);
    }
}
