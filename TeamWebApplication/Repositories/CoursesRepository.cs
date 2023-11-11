using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;

namespace TeamWebApplication.Repositories
{
    public class CoursesRepository
    {
        private readonly ApplicationDBContext _db;
        public CoursesRepository(ApplicationDBContext db)
        {
            _db = db;
        }


    }
}
