using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;

namespace TeamWebApplication.Repositories
{
    public class CourseUsersRepository
    {
        private readonly ApplicationDBContext _db;
        public CourseUsersRepository(ApplicationDBContext db)
        {
            _db = db;
        }


    }
}
