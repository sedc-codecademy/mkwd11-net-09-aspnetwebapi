namespace Profiles.BLL.Services
{
    public interface IEmailSender
    {
        void SendConfirmationEmail(string email, string code);
    }
}