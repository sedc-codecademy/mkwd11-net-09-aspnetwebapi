namespace SEDC.NotesAppFinal.DTOs.UserDtos
{
    public class UserDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Username { get; set; } = string.Empty;

        public int Age { get; set; }
    }
}
