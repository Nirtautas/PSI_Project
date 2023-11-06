namespace TeamWebApplication.Controllers.ControllerEventArgs
{
    public class AttendanceEventArgs
    {
        public string UserEmail { get; }
        public string UserName { get; }
        public string CourseName { get; }
        public bool AddedOrRemoved { get; }

        public AttendanceEventArgs(string userEmail, string userName, string courseName, bool addedOrRemoved)
        {
            UserEmail = userEmail;
            UserName = userName;
            CourseName = courseName;
            AddedOrRemoved = addedOrRemoved;
        }
    }
}
