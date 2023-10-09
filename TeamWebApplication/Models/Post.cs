using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebApplication.Models
{
    [Table("Posts")]
    public abstract class Post : IComparable<Post>
    {
        [Key]
        public int PostId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsVisible { get; set; }
        [NotMapped]
        public PostType PostType { get; set; }

        //Database links
        public Course Course { get; set; }

        //Abstract definitions
        public abstract void ApplyData(string? textData);
        public abstract string? DataToString();
        public abstract string? DataToHtml();

        public int CompareTo(Post? other)
        {
            if (PostId > other.PostId || other == null)
                return 1;
            else if (PostId < other.PostId)
                return -1;
            return 0;
        }
    }
    public enum PostType
    {
        Text,
        Link
    }
}
