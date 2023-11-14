using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Controllers.ControllerHandlers;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories.Interfaces;

namespace TeamWebApplication.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly ApplicationDBContext _db;
        public PostsRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Post>> GetPostsByCourseAsync(int courseId)
        {
            return await _db.Posts
                .Where(p => p.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _db.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task InsertPostAsync(Post post)
        {
            await PostHandler.AddPostAsync(_db, post);
            await SaveAsync();
        }

        public async Task DeletePostByIdAsync(int postId)
        {
            Post post = await _db.Posts.FirstOrDefaultAsync(p => p.PostId == postId);

            if (post != null)
            {
                _db.Posts.Remove(post);
                await SaveAsync();
            }
        }

        public async Task UpdateTextPostAsync(TextPost post)
        {
            TextPost existingPost = (TextPost) await _db.Posts.FirstOrDefaultAsync(p => p.PostId == post.PostId);

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

                await SaveAsync();
            }
        }

        public async Task UpdateFilePostAsync(FilePost post)
        {
            FilePost existingPost = (FilePost) await _db.Posts.FirstOrDefaultAsync(p => p.PostId == post.PostId);

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

                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}
