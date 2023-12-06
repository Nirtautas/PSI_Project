namespace TeamWebApplicationAPI.Models
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string? CommentatorName { get; set; }
        public string? CommentatorSurname { get; set; }
        public string? UserComment { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
