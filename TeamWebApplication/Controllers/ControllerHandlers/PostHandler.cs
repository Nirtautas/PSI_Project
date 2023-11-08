using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers.ControllerHandlers
{
    public static class PostHandler
    {
        public static void AddPost<T>(ApplicationDBContext db, T post) where T : Post
        {
            db.Posts.Add(post);
            db.SaveChanges();

            //possible update: implementing notification system to inform students about a new post/assignment (on click)
        }
    }
}