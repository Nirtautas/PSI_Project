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

        public async Task DeleteCommentByIdAsync(int? commentId)
        {
            if (commentId == null)
                throw new ArgumentNullException(nameof(commentId));

            var comment = await _db.Comments.FindAsync(commentId);

            if (comment != null)
            {
                _db.Comments.Remove(comment);
                await SaveAsync();
            }
        }

        public async Task DeleteCommentAsync(Comment? comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            _db.Comments.Remove(comment);
            await SaveAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int? commentId)
        {
            if (commentId == null)
                throw new ArgumentNullException(nameof(commentId));

            return await _db.Comments.FindAsync(commentId);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByCourseIdAsync(int? courseId)
        {
            if (courseId == null)
                throw new ArgumentNullException(nameof(courseId));

            return await _db.Comments
                .Where(comment => comment.CourseId == courseId)
                .OrderByDescending(comment => comment.CreationTime)
                .ToListAsync();
        }

        public async Task InsertCommentAsync(Comment? comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            await _db.Comments.AddAsync(comment);
            await SaveAsync();
        }

        public async Task UpdateCommentAsync(int? commentId, string? comment)
        {
            if (commentId == null)
                throw new ArgumentNullException(nameof(commentId));
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            var existingComment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == commentId);
            if (existingComment != null)
            {
                if (existingComment.UserComment != comment)
                {
                    existingComment.CreationTime = DateTime.Now;
                }
                existingComment.UserComment = comment;
                _db.Comments.Update(existingComment);
            }
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
