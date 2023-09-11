using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MoviesApp.Services
{
    public interface IUserService
    {
        void Register(RegisterUserDto registerUserDto);
        UserDto Login(LoginUserDto loginUserDto);
    }
}
