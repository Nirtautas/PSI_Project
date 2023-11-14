using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetUserByIdAsync(int? id);
        Task<IEnumerable<User>> GetUsersInCourse(int? courseId);
        Task InsertUserAsync(User user);
        Task DeleteUserByIdAsync(int id);
        Task UpdateUserAsync(User user);
        Task SaveAsync();
    }
}
