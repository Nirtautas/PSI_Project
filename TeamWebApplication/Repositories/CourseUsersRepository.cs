using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories.Interfaces;

namespace TeamWebApplication.Repositories
{
    public class CourseUsersRepository : ICourseUsersRepository
    {
        private readonly ApplicationDBContext _db;
        public CourseUsersRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<int>> GetUsersByCourseIdAsync(int? courseId)
        {
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            return await _db.CoursesUsers
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.UserId)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetCoursesByUserIdAsync(int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var courseIds = await _db.CoursesUsers
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CourseId)
                .ToListAsync();
            return courseIds;
        }

        public async Task<bool> CheckIfRelationExistsAsync(int? courseId, int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            return await _db.CoursesUsers.AnyAsync(row => row.UserId == userId && row.CourseId == courseId);
        }

        public async Task InsertRelationAsync(int? courseId, int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            var exists = await _db.CoursesUsers.AnyAsync(cu => cu.CourseId == courseId && cu.UserId == userId);
            if (!exists)
            {
                await _db.CoursesUsers.AddAsync(new CourseUser { CourseId = (int)courseId, UserId = (int)userId });
                await SaveAsync();
            }
        }

        public async Task DeleteRelationAsync(int? courseId, int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            CourseUser courseUser = await _db.CoursesUsers.FirstOrDefaultAsync(cu => cu.CourseId == courseId && cu.UserId == userId);

            if (courseUser != null)
            {
                _db.CoursesUsers.Remove(courseUser);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }
    }

}
