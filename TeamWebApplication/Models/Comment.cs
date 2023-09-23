using System.Globalization;

namespace TeamWebApplication.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public string UsersNameThatCommented { get; set; }
        public string UsersSurnameThatCommented { get; set; }
        public string UserComment { get; set; }
        public DateTime CommentCreationTime { get; set; } = DateTime.Now;

        public Comment()
        {
        }

        public Comment(
        int commentId,
        int courseId,
        string usersNameThatCommented,
        string usersSurnameThatCommented,
        DateTime commentCreationTime,
        string userComment)
        {
            CommentId = commentId;
            CourseId = courseId;
            UsersNameThatCommented = usersNameThatCommented;
            UsersSurnameThatCommented = usersSurnameThatCommented;
            CommentCreationTime = commentCreationTime;
            UserComment = userComment;
        }

        public override string ToString()
        {
            return
                CommentId.ToString() + ";" +
                CourseId.ToString() + ";" +
                UsersNameThatCommented + ";" +
                UsersSurnameThatCommented + ";" +
                CommentCreationTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + ";" +
                CommentCreationTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ";" +
                UserComment;
        }
    }
}
