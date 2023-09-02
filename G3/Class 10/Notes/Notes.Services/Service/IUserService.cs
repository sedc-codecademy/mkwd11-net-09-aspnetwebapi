using Notes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Service
{
    public interface IUserService
    {
        RegisterResult Register(string email, string password, string name);

        string Login(string email, string password);
    }
}
