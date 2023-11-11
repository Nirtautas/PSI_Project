using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;

namespace TeamWebApplication.Repositories
{
    public class UsersRepository
    {
        private readonly ApplicationDBContext _db;
        public UsersRepository(ApplicationDBContext db)
        {
            _db = db;
        }


    }
}
