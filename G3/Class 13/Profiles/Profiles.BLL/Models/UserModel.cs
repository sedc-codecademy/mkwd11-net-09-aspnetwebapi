namespace Profiles.BLL.Models
{
    public class UserModel
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
