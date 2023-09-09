using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Mappers.Users
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto userDto, string hash)
        {
            return new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                //Password = userDto.Password //password must not be kept as plain text in db!!!!!
                Password = hash,
                Role = userDto.Role
            };
        }
    }
}
