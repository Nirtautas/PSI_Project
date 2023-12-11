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
        private readonly INullCheckInterceptor<int> _nullCheckInterceptorForInt;
        private readonly INullCheckInterceptor<Comment> _nullCheckInterceptorForComment;
        private readonly INullCheckInterceptor<string> _nullCheckInterceptorForString;
        private readonly IUpdateNeededInterceptor _updateNeededInterceptor;

        public CommentsRepository(ApplicationDBContext db, INullCheckInterceptor<int> nullCheckInterceptorForInt, INullCheckInterceptor<Comment> nullCheckInterceptorForComment, INullCheckInterceptor<string> nullCheckInterceptorForString, IUpdateNeededInterceptor updateNeededInterceptor)
        {
            _db = db;
            _nullCheckInterceptorForInt = nullCheckInterceptorForInt;
            _nullCheckInterceptorForComment = nullCheckInterceptorForComment;
            _nullCheckInterceptorForString = nullCheckInterceptorForString;
            _updateNeededInterceptor = updateNeededInterceptor;
        }

        public async Task DeleteCommentByIdAsync(int? commentId)
        {
            _nullCheckInterceptorForInt.CheckIfNotNull(commentId);

            var comment = await _db.Comments.FindAsync(commentId);
            _nullCheckInterceptorForComment.CheckIfNotNull(comment);

            _db.Comments.Remove(comment);
            await SaveAsync();
        }

        public async Task DeleteCommentAsync(Comment? comment)
        {
            _nullCheckInterceptorForComment.CheckIfNotNull(comment);

            _db.Comments.Remove(comment);
            await SaveAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int? commentId)
        {
            _nullCheckInterceptorForInt.CheckIfNotNull(commentId);

            return await _db.Comments.FindAsync(commentId);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByCourseIdAsync(int? courseId)
        {
            _nullCheckInterceptorForInt.CheckIfNotNull(courseId);

            return await _db.Comments
                .Where(comment => comment.CourseId == courseId)
                .OrderByDescending(comment => comment.CreationTime)
                .ToListAsync();
        }

        public async Task InsertCommentAsync(Comment? comment)
        {
            _nullCheckInterceptorForComment.CheckIfNotNull(comment);

            await _db.Comments.AddAsync(comment);
            await SaveAsync();
        }

        public async Task UpdateCommentAsync(int? commentId, string? comment)
        {
            _nullCheckInterceptorForInt.CheckIfNotNull(commentId);
            _nullCheckInterceptorForString.CheckIfNotNull(comment);

            var existingComment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == commentId);
            _nullCheckInterceptorForComment.CheckIfNotNull(existingComment);

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