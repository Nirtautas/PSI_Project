using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data.Database;
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

        public async Task DeleteCourseByIdAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            
            var courseToDelete = await _db.Courses.FindAsync(id);

            if (courseToDelete != null)
            {
                _db.Courses.Remove(courseToDelete);
                await SaveAsync();
            }
        }

        public async Task<Course> GetCourseByIdAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return await _db.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _db.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetPublicCoursesAsync()
        {
            return await (
                from course in _db.Courses
                where course.IsPublic == true
                select course
           ).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(int? loggedInUserId)
        {
            if (loggedInUserId == null)
                throw new ArgumentNullException(nameof(loggedInUserId));

            return await (
                from user in _db.Users
                join userCourse in _db.CoursesUsers
                on user.UserId equals userCourse.UserId
                join course in _db.Courses
                on userCourse.CourseId equals course.CourseId
                where user.UserId == loggedInUserId
                select course
           ).ToListAsync();
        }

        public async Task InsertCourseAsync(Course? course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            await _db.Courses.AddAsync(course);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
             await _db.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course? course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            var originalCourse = await _db.Courses.FindAsync(course.CourseId);
            originalCourse.Name = course.Name;
            originalCourse.IsVisible = course.IsVisible;
            originalCourse.IsPublic = course.IsPublic;
            originalCourse.Description = course.Description;
            _db.Update(originalCourse);
            await SaveAsync();
        }
    }
}
