using System.Globalization;

namespace TeamWebApplication.Models
{
    public class Comment : IComparable<Comment>
    {
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
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
        int userId,
        string usersNameThatCommented,
        string usersSurnameThatCommented,
        DateTime commentCreationTime,
        string userComment)
        {
            CommentId = commentId;
            CourseId = courseId;
            UserId = userId;
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
                UserId.ToString() + ";" +
                UsersNameThatCommented + ";" +
                UsersSurnameThatCommented + ";" +
                CommentCreationTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + ";" +
                UserComment;
        }

        public int CompareTo(Comment? other)
        {
            if (CommentId > other.CommentId || other == null)
                return 1;
            else if (CommentId < other.CommentId)
                return -1;
            return 1;
        }
    }
}
