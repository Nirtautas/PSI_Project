using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;
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

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task InsertUserAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await SaveAsync();
        }

        public async Task DeleteUserByIdAsync(int userId)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null)
            {
                _db.Users.Remove(user);
                await SaveAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Surname = user.Email;
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
