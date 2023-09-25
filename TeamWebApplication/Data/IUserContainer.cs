using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface IUserContainer
    {
        void FetchUsers(IRelationContainer relationContainer);
        void PrintUserList();
        void WriteUsers();
        public void AddRelation(int userId, int courseId);
        public void CreateUser(User user);

        ICollection<User> userList { get; }
        int loggedInUserId { get; set; }
    }
}
