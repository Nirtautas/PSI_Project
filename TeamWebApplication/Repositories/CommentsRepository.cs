using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models;
using TeamWebApplication.Repositories.Interfaces;

namespace TeamWebApplication.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDBContext _db;

        public CommentsRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task DeleteCommentByIdAsync(int commentId)
        {
            Comment comment = await _db.Comments.FindAsync(commentId);
            _db.Comments.Remove(comment);
        }

        public async Task<Comment> GetCommentByIdAsync(int commentId)
        {
            return await _db.Comments.FindAsync(commentId);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByCourseIdAsync(int courseId)
        {
            return await _db.Comments
                .Where(comment => comment.CourseId == courseId)
                .ToListAsync();
        }

        public async Task InsertCommentAsync(Comment comment)
        {
            await _db.Comments.AddAsync(comment);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _db.Entry(comment).State = EntityState.Modified;
            await Task.CompletedTask;
        }

    }
}
