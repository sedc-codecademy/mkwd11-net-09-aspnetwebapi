namespace SEDC.NotesAppFinal.DTOs.UserDtos
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmedPassword { get; set; } = string.Empty;
    }
}
