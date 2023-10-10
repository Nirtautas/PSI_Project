using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface IUserContainer
    {
        ICollection<User> userList { get; }
        int loggedInUserId { get; set; }
        Role? loggedInUserRole { get; set; }
        public int currentCourseId { get; set; }
    }
}
