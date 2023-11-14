using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByCourseIdAsync(int id);
        Task<Comment> GetCommentByIdAsync(int id);
        Task InsertCommentAsync(Comment comment);
        Task DeleteCommentByIdAsync(int id);
        Task UpdateCommentAsync(Comment comment);
        Task SaveAsync();
    }
}
