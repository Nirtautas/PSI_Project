using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;

namespace TeamWebApplication.Controllers.ControllerHandlers
{
    public static class PostHandler
    {
        public static async Task AddPostAsync<T>(ApplicationDBContext db, T post) where T : Post
        {
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();

            //possible update: implementing notification system to inform students about a new post/assignment (on click)
        }
    }
}