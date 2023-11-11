using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        IEnumerable<Comment> GetCommentsByCourseId(int id);
        Comment GetCommentById(int id);
        void InsertComment(Comment comment);
        void DeleteCommentById(int id);
        void UpdateComment(Comment comment);
        void Save();
    }
}
