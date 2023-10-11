using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface IUserContainer
    {
        void FetchUsers(IRelationContainer relationContainer);
        User? GetUser(int userId);
        void PrintUserList();
        void WriteUsers();
        void AddRelation(int userId, int courseId);
        void DeleteRelation (int userId, int courseId);
        void CreateUser(User user);

        ICollection<User> userList { get; }
        int LoggedInUserId { get; set; }
        Role? LoggedInUserRole { get; set; }
        int CurrentCourseId { get; set; }
    }
}
