
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebApplication.Models
{
	[Table("Comments")]
	public class Comment : IComparable<Comment>
    {
        [Key]
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string CommentatorName { get; set; }
        public string CommentatorSurname { get; set; }
        public string UserComment { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;

        public Comment()
        {
        }

        public Comment(
        int commentId,
        int courseId,
        int userId,
        string usersNameThatCommented,
        string usersSurnameThatCommented,
        string userComment)
        {
            CommentId = commentId;
            CourseId = courseId;
            UserId = userId;
            CommentatorName = usersNameThatCommented;
            CommentatorSurname = usersSurnameThatCommented;
            CreationTime = DateTime.Now;
            UserComment = userComment;
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
