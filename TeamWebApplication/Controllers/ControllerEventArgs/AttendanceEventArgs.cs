using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Controllers.ControllerEventArgs
{
    public class AttendanceEventArgs
    {
        public User User { get; }
        public Course Course { get; }
        public bool AddedOrRemoved { get; }

        public AttendanceEventArgs(User user, Course course, bool addedOrRemoved)
        {
            User = user;
            Course = course;
            AddedOrRemoved = addedOrRemoved;
        }
    }
}
