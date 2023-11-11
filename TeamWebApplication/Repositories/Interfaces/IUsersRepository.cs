using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        User GetUserById(int id);
        void InsertUser(Post post);
        void DeleteUserById(int id);
        void UpdateUser(User user);
        void Save();
    }
}
