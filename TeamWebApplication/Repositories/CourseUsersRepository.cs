using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;
using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories
{
    public class CourseUsersRepository
    {
        private readonly ApplicationDBContext _db;
        public CourseUsersRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public IEnumerable<int> GetUsersByCourseId(int courseId)
        {
            return _db.CoursesUsers
                .Where(uc => uc.CourseId == courseId)
                .Select(uc => uc.UserId)
                .ToList();
        }

        public IEnumerable<int> GetCoursesByUserId(int userId)
        {
            var courseIds = _db.CoursesUsers
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CourseId)
                .ToList();

            return courseIds;
        }



        public void InsertRelation(int courseId, int userId)
        { 
            if (!_db.CoursesUsers.Any(cu => cu.CourseId == courseId && cu.UserId == userId))
            {
                _db.CoursesUsers.Add(new CourseUser { CourseId = courseId, UserId = userId });
                Save();
            }
        }

        public void DeleteRelation(int courseId, int userId)
        {
            CourseUser courseUser = _db.CoursesUsers.FirstOrDefault(cu => cu.CourseId == courseId && cu.UserId == userId);

            if (courseUser != null)
            {
                _db.CoursesUsers.Remove(courseUser);
                Save();
            }
        }
        public void Save()
        {
           _db.SaveChanges();
        }
    }

}
