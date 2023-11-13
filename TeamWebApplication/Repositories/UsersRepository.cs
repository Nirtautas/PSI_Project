using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;
using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories
{
    public class UsersRepository
    {
        private readonly ApplicationDBContext _db;
        public UsersRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public User GetUserById(int userId)
        {
            return _db.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public void InsertUser(User user)
        {
            _db.Users.Add(user);
            Save();
        }

        public void DeleteUserById(int userId)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                _db.Users.Remove(user);
                Save();
            }
        }

        public void UpdateUser(User user)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.UserId == user.UserId);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Surname = user.Email;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;
                existingUser.Faculty = user.Faculty;
                existingUser.Specialization = user.Specialization;

                Save();
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
