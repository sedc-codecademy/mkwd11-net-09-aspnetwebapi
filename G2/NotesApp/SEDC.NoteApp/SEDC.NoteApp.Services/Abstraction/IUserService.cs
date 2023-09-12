using SEDC.NoteApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.Services.Abstraction
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);
        string LoginUser(LoginUserDto loginUserDto);
    }
}
