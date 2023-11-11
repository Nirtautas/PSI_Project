using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICoursesRepository
    {
        IEnumerable<Course> GetCourses();
        Course GetCourseById(int id);
        void InsertCourse(Comment comment);
        void DeleteCourseById(int id);
        void UpdateCourse(Comment comment);
        void Save();
    }
}
