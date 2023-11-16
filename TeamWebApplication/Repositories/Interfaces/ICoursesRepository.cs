using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<IEnumerable<Course>> GetPublicCoursesAsync();
        Task<Course> GetCourseByIdAsync(int? id);
        Task InsertCourseAsync(Course? course);
        Task DeleteCourseByIdAsync(int? id);
        Task<IEnumerable<Course>> GetCoursesByUserIdAsync(int? loggedInUserId);
        Task UpdateCourseAsync(Course? course);
        Task SaveAsync();
    }
}
