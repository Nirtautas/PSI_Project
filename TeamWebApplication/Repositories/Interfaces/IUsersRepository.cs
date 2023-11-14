using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetUserByIdAsync(int? id);
        Task<IEnumerable<User>> GetUsersInCourseAsync(int? courseId);
        Task<User?> GetUserByCredentialsAsync(int? userId, string? password);
        Task<bool> UserWithSuchEmailExistsAsync(string? email);
        Task<User?> GetUserByEmailAsync(string? email);
        Task InsertUserAsync(User user);
        Task DeleteUserByIdAsync(int id);
        Task DeleteUserAsync(User? user);
        Task UpdateUserAsync(User user);
        Task SaveAsync();
    }
}
