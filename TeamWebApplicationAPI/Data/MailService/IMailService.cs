using TeamWebApplicationAPI.Controllers.ControllerEventArgs;

namespace TeamWebApplicationAPI.Data.MailService
{
    public interface IMailService
    {
        public void OnRegistration(object source, RegistrationEventArgs e);
        public void OnAttendanceChange(object source, AttendanceEventArgs e);
    }
}
