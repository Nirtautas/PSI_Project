﻿using Microsoft.AspNetCore.Mvc;
using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface ICommentContainer
    {
        void FetchComments();
        void PrintComments();
        void WriteComments();
        void CreateComment(Comment comment, int CurrentCourseId, int LoggedInUserId, IUserContainer _userContainer);
        void DeleteComment(Comment comment);
        ICollection<Comment> CommentList { get; }
    }
}
