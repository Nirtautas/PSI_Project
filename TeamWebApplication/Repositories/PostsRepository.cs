using TeamWebApplication.Controllers.ControllerHandlers;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;
using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories
{
    public class PostsRepository
    {
        private readonly ApplicationDBContext _db;
        public PostsRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public IEnumerable<Post> GetPostsByCourse(int courseId)
        {
            return _db.Posts
                .Where(p => p.CourseId == courseId)
                .ToList();
        }

        public Post GetPostById(int postId)
        {
            return _db.Posts.FirstOrDefault(p => p.PostId == postId);
        }

        public void InsertPost(Post post)
        {
            PostHandler.AddPost(_db, post);
            Save();
        }

        public void DeletePostById(int postId)
        {
            Post post = _db.Posts.FirstOrDefault(p => p.PostId == postId);

            if (post != null)
            {
                _db.Posts.Remove(post);
                Save();
            }
        }

        public void UpdateTextPost(TextPost post)
        {
            TextPost existingPost = (TextPost)_db.Posts.FirstOrDefault(p => p.PostId == post.PostId);

            if (existingPost != null)
            {
                if (existingPost.TextContent != post.TextContent || existingPost.Name != post.Name)
                {
                    existingPost.CreationDate = DateTime.Now;
                }
                existingPost.Name = post.Name;
                existingPost.IsVisible = post.IsVisible;
                existingPost.PostType = post.PostType;
                existingPost.TextContent = post.TextContent;

                Save();
            }
        }

        public void UpdateFilePost(FilePost post)
        {
            FilePost existingPost = (FilePost)_db.Posts.FirstOrDefault(p => p.PostId == post.PostId);

            if (existingPost != null)
            {
                if (existingPost.Name != post.Name)
                {
                    existingPost.CreationDate = DateTime.Now;
                }
                existingPost.Name = post.Name;
                existingPost.IsVisible = post.IsVisible;
                existingPost.PostType = post.PostType;
                existingPost.FileName = post.FileName;

                Save();
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
