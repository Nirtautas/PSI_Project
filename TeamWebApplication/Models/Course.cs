using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebApplication.Models
{
    [Table("Courses")]
    public class Course : IComparable<Course>
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public bool IsPublic { get; set; }

        //Database links
        public ICollection<CourseUser> Users { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }

        public Course()
        {

        }

        public Course(int id, string name = "dname", string description = "ddescription",
            bool IsVisible = false, bool isPublic = false)
        {
            this.CourseId = id;
            this.Name = name;
            this.CreationDate = DateTime.Now;
            this.Description = description;
            this.IsVisible = IsVisible;
            this.IsPublic = isPublic;
        }

        public int CompareTo(Course? other)
        {
            if (CourseId > other.CourseId || other == null)
                return 1;
            else if (CourseId < other.CourseId)
                return -1;
            return 0;
        }
    }
}
