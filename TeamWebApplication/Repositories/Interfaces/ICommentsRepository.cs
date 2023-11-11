using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICommentsRepository
    {
        IEnumerable<Comment> GetComments();
        Comment GetCommentById(int id);
        void InsertComment(Comment comment);
        void DeleteCommentById(int id);
        void UpdateComment(Comment comment);
        void Save();
    }
}
