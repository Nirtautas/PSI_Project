using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Reflection.Metadata;
using TeamWebApplication.Data.Database;
using TeamWebApplicationAPI.Models;
using TeamWebApplication.Repositories.Interfaces;

namespace TeamWebApplication.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDBContext _db;
        public UsersRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<User?> GetUserByIdAsync(int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetUsersInCourseAsync(int? courseId)
        {
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            return await (
                from user in _db.Users
                join userCourse in _db.CoursesUsers
                on user.UserId equals userCourse.UserId
                join course in _db.Courses
                on userCourse.CourseId equals course.CourseId
                where course.CourseId == courseId
                select user
            ).ToListAsync();
        }

        public async Task<User?> GetUserByCredentialsAsync(int? userId, string? password)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            return await _db.Users.FirstOrDefaultAsync(user => user.UserId == userId && user.Password == password);
        }

        public async Task<bool> UserWithSuchEmailExistsAsync(string? email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));
            return await _db.Users.AnyAsync(tuser => tuser.Email == email);
        }

        public async Task<User?> GetUserByEmailAsync(string? email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));
            return await _db.Users.SingleOrDefaultAsync(tuser => tuser.Email == email);
        }

        public async Task InsertUserAsync(User? user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _db.Users.AddAsync(user);
            await SaveAsync();
        }

        public async Task DeleteUserByIdAsync(int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null)
            {
                _db.Users.Remove(user);
                await SaveAsync();
            }
        }

        public async Task DeleteUserAsync(User? user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _db.Users.Remove(user);
            await SaveAsync();
        }

        public async Task UpdateUserAsync(User? user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;
                existingUser.Faculty = user.Faculty;
                existingUser.Specialization = user.Specialization;

                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
