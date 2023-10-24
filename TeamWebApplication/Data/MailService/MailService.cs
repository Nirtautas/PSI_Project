using System.Net;
using System.Net.Mail;
using TeamWebApplication.Data.ExceptionLogger;

namespace TeamWebApplication.Data.MailService
{
    public class MailService : IMailService
    {
        private readonly IExceptionLogger _logger;

        public MailService(IExceptionLogger logger)
        {
            _logger = logger;
        }

        public void SendConfirmationEmail(string userEmail, string userName, int userId)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("MudliTeam.dev@outlook.com");
            mailMessage.To.Add(userEmail);
            mailMessage.Subject = $"Welcome to Mudli {userName}!";
            mailMessage.Body = $"Welcome to Mudli! Your user identification code is {userId}. Happy learning!";

            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("MudliTeam.dev@outlook.com", "MudliTeam1!!")
            };

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }
    }
}
