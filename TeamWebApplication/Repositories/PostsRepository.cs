using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;

namespace TeamWebApplication.Repositories
{
    public class PostsRepository
    {
        private readonly ApplicationDBContext _db;
        public PostsRepository(ApplicationDBContext db)
        {
            _db = db;
        }


    }
}
