using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Controllers.ControllerEventArgs
{
    public class RegistrationEventArgs : EventArgs
    {
        public User User { get; }

        public RegistrationEventArgs(User user)
        {
            User = user;
        }
    }
}
