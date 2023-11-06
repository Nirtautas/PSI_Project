using System.Net;
using System.Net.Mail;
using TeamWebApplication.Controllers;
using TeamWebApplication.Controllers.ControllerEventArgs;
using TeamWebApplication.Data.ExceptionLogger;

namespace TeamWebApplication.Data.MailService
{
    public class MailService : IMailService
    {
        private readonly IDataLogger _logger;

        public MailService(IDataLogger logger)
        {
            _logger = logger;
        }

        public void OnRegistration(object source, RegistrationEventArgs e)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("MudliTeam.dev@outlook.com");
            mailMessage.To.Add(e.UserEmail);
            mailMessage.Subject = $"Welcome to Mudli {e.UserName}!";
            mailMessage.Body = $"Welcome to Mudli! Your user identification code is {e.UserId}. Happy learning!";

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

        public void OnAttendanceChange(object source, AttendanceEventArgs e)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("MudliTeam.dev@outlook.com");
            mailMessage.To.Add(e.UserEmail);
            if (e.AddedOrRemoved)
            {
                mailMessage.Subject = $"You have been added to course {e.CourseName}!";
                mailMessage.Body = $"Dear {e.UserName}," + Environment.NewLine + $"You have been added to course {e.CourseName}!";
            }
            else
            {
                mailMessage.Subject = $"You have been removed from course {e.CourseName}!";
                mailMessage.Body = $"Dear {e.UserName}," + Environment.NewLine + $"You have been removed from course {e.CourseName}!";
            }

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
