namespace TeamWebApplication.Models
{
    public class CourseAndComment
    {
        public IEnumerable<Post> PostData { get; set; }
        public IEnumerable<Comment> CommentData { get; set; }
        public ICollection<Course> CourseData { get; set; }
        public ICollection<User> UserData { get; set; }
        public int LoggedInUser { get; set; }
        public Comment comment { get; set; }
    }
}
