using System.Globalization;
using TeamWebApplication.Models;

namespace TeamWebApplication.ExtensionMethods
{
    public static class CommentExtension
    {
        public static string FormattedToString(this Comment comment)
        {
            return
              comment.CommentId.ToString() + ";" +
              comment.CourseId.ToString() + ";" +
              comment.UserId.ToString() + ";" +
              comment.UsersNameThatCommented + ";" +
              comment.UsersSurnameThatCommented + ";" +
              comment.CommentCreationTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + ";" +
              comment.UserComment;
        }
    }
}