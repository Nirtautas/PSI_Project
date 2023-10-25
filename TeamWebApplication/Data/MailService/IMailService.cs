namespace TeamWebApplication.Data.MailService
{
    public interface IMailService
    {
        public void SendConfirmationEmail(string userEmail, string userName, int userId);
    }
}
