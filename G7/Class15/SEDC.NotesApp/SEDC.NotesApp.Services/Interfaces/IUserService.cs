﻿using SEDC.NotesApp.Dtos.Notes;
using SEDC.NotesApp.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto registerUserDto);
        UserDto LoginUser(LoginUserDto loginUserDto);
    }
}
