using SEDC.NotesAppFinal.Domain.Models;
using SEDC.NotesAppFinal.DTOs.UserDtos;

namespace SEDC.NotesAppFinal.Mappers
{
    public static class UserMappers
    {
        public static UserDto MapToUserDto(this User user)
        {
            return new UserDto()
            {
                Age = user.Age,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username
            };
        }
    }
}
