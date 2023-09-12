namespace SEDC.NotesApp.Dtos.Users
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
    }
}
