namespace TeamWebApplication.Controllers.ControllerEventArgs
{
    public class RegistrationEventArgs : EventArgs
    {
        public string UserEmail { get; }
        public string UserName { get; }
        public int UserId { get; }

        public RegistrationEventArgs(string userEmail, string userName, int userId)
        {
            UserEmail = userEmail;
            UserName = userName;
            UserId = userId;
        }
    }
}
