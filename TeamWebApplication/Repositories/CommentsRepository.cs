using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;
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

        public void DeleteCommentById(int commentId)
        {
            Comment comment = _db.Comments.Find(commentId);
            _db.Comments.Remove(comment);
        }

        public Comment GetCommentById(int commentId)
        {
            return _db.Comments.Find(commentId);
        }

        public IEnumerable<Comment> GetComments()
        {
            return _db.Comments.ToList();
        }

        public void InsertComment(Comment comment)
        {
            _db.Comments.Add(comment);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void UpdateComment(Comment comment)
        {
            _db.Entry(comment).State = EntityState.Modified;
        }

    }
}
