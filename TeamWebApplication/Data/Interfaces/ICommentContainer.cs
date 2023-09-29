using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface ICommentContainer
    {
        void FetchComments();
        void PrintComments();
        void WriteComments();
        void CreateComment(Comment comment, int currentCourseId, int loggedInUserId, IUserContainer _userContainer);
        void DeleteComment(Comment comment);
        ICollection<Comment> CommentList { get; }
    }
}
