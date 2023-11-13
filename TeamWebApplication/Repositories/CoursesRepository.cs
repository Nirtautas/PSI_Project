using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories.Interfaces;

namespace TeamWebApplication.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly ApplicationDBContext _db;
        public CoursesRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public void DeleteCourseById(int id)
        {
            var courseToDelete = _db.Courses.Find(id);

            if (courseToDelete != null)
            {
                _db.Courses.Remove(courseToDelete);
                Save();
            }
        }

        public Course GetCourseById(int id)
        {
            return _db.Courses.Find(id);
        }

        public IEnumerable<Course> GetCourses()
        {
            return _db.Courses.ToList();
        }

        public void InsertCourse(Course course)
        {
            _db.Courses.Add(course);
            Save();
        }

        public void Save()
        {
             _db.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            Course? originalCourse = _db.Courses.Find(course.CourseId);
            originalCourse.Name = course.Name;
            originalCourse.IsVisible = course.IsVisible;
            originalCourse.IsPublic = course.IsPublic;
            originalCourse.Description = course.Description;
            _db.Update(originalCourse);
            _db.SaveChanges();
        }
    }
}
