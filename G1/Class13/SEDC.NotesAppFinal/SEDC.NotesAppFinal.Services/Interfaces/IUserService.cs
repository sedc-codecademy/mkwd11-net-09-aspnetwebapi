using SEDC.NotesAppFinal.DTOs.UserDtos;

namespace SEDC.NotesAppFinal.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);

        //This returns string because it will return the JWT Token, that token will be passed with every request
        Task<string> LoginUserAsync(LoginUserDto loginUserDto);
    }
}
