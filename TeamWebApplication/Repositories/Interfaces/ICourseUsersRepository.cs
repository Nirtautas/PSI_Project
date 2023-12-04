using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICourseUsersRepository
    {
        Task<IEnumerable<int>> GetUsersByCourseIdAsync(int? courseId);
        Task<IEnumerable<int>> GetCoursesByUserIdAsync(int? userId);
        Task<bool> CheckIfRelationExistsAsync(int? courseId, int? userId);
        Task InsertRelationAsync(int? courseId, int? userId);
        Task DeleteRelationAsync(int? courseId, int? userId);
        Task SaveAsync();
    }
}
