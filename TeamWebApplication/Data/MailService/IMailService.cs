using TeamWebApplication.Controllers.ControllerEventArgs;

namespace TeamWebApplication.Data.MailService
{
    public interface IMailService
    {
        public void OnRegistration(object source, RegistrationEventArgs e);
        public void OnAttendanceChange(object source, AttendanceEventArgs e);
    }
}
