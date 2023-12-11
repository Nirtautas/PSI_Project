namespace TeamWebApplicationAPI.Models
{
    public class CourseAndCommentDto
    {
        public ICollection<TextPostDto> TextPostData { get; set; }
        public ICollection<FilePostDto> FilePostData { get; set; }
        public IEnumerable<CommentDto> CommentData { get; set; }
        public IEnumerable<CourseUserDto> CourseUserData { get; set; }
        public int LoggedInUser { get; set; }
        public CommentDto Comment { get; set; }
        public UserDto User { get; set; }
        public String CourseName { get; set; }
        public Double CourseEval { get; set; }
    }
}
