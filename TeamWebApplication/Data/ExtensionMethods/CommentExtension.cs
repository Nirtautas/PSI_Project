using System.Globalization;
using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Data.ExtensionMethods
{
    public static class CommentExtension
    {
        public static string FormattedToString(this Comment comment)
        {
            return
              comment.CommentId.ToString() + ";" +
              comment.CourseId.ToString() + ";" +
              comment.UserId.ToString() + ";" +
              comment.CommentatorName + ";" +
              comment.CommentatorSurname + ";" +
              comment.CreationTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + ";" +
              comment.UserComment;
        }
    }
}