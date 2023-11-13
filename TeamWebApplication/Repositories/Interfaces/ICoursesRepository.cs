using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICoursesRepository
    {
        IEnumerable<Course> GetCourses();
        Course GetCourseById(int id);
        void InsertCourse(Course course);
        void DeleteCourseById(int id);
        void UpdateCourse(Course course);
        void Save();
    }
}
