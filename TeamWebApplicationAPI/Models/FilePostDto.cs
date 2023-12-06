using TeamWebApplicationAPI.Models.Enums;

namespace TeamWebApplicationAPI.Models
{
    public class FilePostDto
    {
        public int PostId { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsVisible { get; set; }
        public PostType PostType { get; set; }
        public string? FileName { get; set; }
    }
}
