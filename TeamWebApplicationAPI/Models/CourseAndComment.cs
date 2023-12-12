namespace TeamWebApplicationAPI.Models
{
    public class CourseAndComment
    {
        public ICollection<Post> PostData { get; set; } = new List<Post>();
        public ICollection<TextPost> TextPostData { get; set; }
        public ICollection<FilePost> FilePostData { get; set; }
        public IEnumerable<Comment> CommentData { get; set; }
        public IEnumerable<CourseUser> CourseUserData { get; set; }
        public int LoggedInUser { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
        public String CourseName { get; set; }
        public Double CourseEval { get; set; }
    }
}
