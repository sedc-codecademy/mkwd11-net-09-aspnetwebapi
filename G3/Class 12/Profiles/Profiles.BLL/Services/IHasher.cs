namespace Profiles.BLL.Services
{
    public interface IHasher
    {
        string HashPassword(string password);

        bool ValidatePassword(string password, string hash);
    }
}