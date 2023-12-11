using Microsoft.EntityFrameworkCore;
using TeamWebApplicationAPI.Data.Database;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interceptors.Interfaces;
using TeamWebApplicationAPI.Repositories.Interfaces;

namespace TeamWebApplicationAPI.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly INullCheckInterceptor _nullCheckInterceptor;
        private readonly IUpdateNeededInterceptor _updateNeededInterceptor;

        public CommentsRepository(ApplicationDBContext db, INullCheckInterceptor nullCheckInterceptor, IUpdateNeededInterceptor updateNeededInterceptor)
        {
            _db = db;
            _nullCheckInterceptor = nullCheckInterceptor;
            _updateNeededInterceptor = updateNeededInterceptor;
        }

        public async Task DeleteCommentByIdAsync(int? commentId)
        {
            _nullCheckInterceptor.CheckId(commentId);

            var comment = await _db.Comments.FindAsync(commentId);
            _nullCheckInterceptor.CheckForNullValues(comment);

            _db.Comments.Remove(comment);
            await SaveAsync();
        }

        public async Task DeleteCommentAsync(Comment? comment)
        {
            _nullCheckInterceptor.CheckForNullValues(comment);

            _db.Comments.Remove(comment);
            await SaveAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int? commentId)
        {
            _nullCheckInterceptor.CheckId(commentId);

            return await _db.Comments.FindAsync(commentId);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByCourseIdAsync(int? courseId)
        {
            _nullCheckInterceptor.CheckId(courseId);

            return await _db.Comments
                .Where(comment => comment.CourseId == courseId)
                .OrderByDescending(comment => comment.CreationTime)
                .ToListAsync();
        }

        public async Task InsertCommentAsync(Comment? comment)
        {
            _nullCheckInterceptor.CheckForNullValues(comment);

            await _db.Comments.AddAsync(comment);
            await SaveAsync();
        }

        public async Task UpdateCommentAsync(int? commentId, string? comment)
        {
            _nullCheckInterceptor.CheckId(commentId);
            _nullCheckInterceptor.CheckString(comment);

            var existingComment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == commentId);
            _nullCheckInterceptor.CheckForNullValues(existingComment);

            if (_updateNeededInterceptor.IsCommentUpdateNeeded(existingComment, comment))
            {
                existingComment.CreationTime = DateTime.Now;
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