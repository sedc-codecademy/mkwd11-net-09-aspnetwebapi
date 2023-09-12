using SEDC.NotesApp.Dtos.Users;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface IUsersService
    {
        void Register(RegisterUserDto registerUserDto);
        string Login(LoginDto loginDto);
    }
}
