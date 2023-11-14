using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task InsertCourseAsync(Course course);
        Task DeleteCourseByIdAsync(int id);
        Task UpdateCourseAsync(Course course);
        Task SaveAsync();
    }
}
