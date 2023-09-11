﻿using NotesAndTagsApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Services.Interfaces
{
    public interface IUserService
    {
        void Register(RegisterUserDto registerUserDto);
        string LoginUser(LoginDto loginDto);
    }
}
