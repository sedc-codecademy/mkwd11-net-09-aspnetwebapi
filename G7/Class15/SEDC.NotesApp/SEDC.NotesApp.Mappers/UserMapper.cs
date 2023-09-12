using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Mappers
{
    public  static class UserMapper
    {
        public static UserDto ToUserDto(this User user, string token)
        {
            return new UserDto()
            {
                Id = user.Id,
                Age = user.Age,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role,
                Token = token,                
            };
        }

        public static User ToUserModel(this RegisterUserDto registerUserDto, string passwordHash)
        {
            return new User()
            {
                Age = registerUserDto.Age,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Password = passwordHash,
                Username = registerUserDto.Username,
                Role = RoleEnum.User,
            };
        }
    }
}
