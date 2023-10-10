using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using TeamWebApplication.Models;

namespace TeamWebApplication.Models
{
    public class Course : IComparable<Course>
    {
        //These variables are fetched from files
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<int>? UsersInCourseId { get; set; }
        //These variables are not yet implemented
        public ICollection<int>? PostsInCourseId { get; set; }

        public Course() {
            UsersInCourseId = new List<int>();
            PostsInCourseId = new List<int>();
        }

        public Course(int id, string name, DateTime creationDate, string description, bool IsVisible, bool isPublic) {
            this.Id = id;
            this.Name = name;
            this.CreationDate = creationDate;
            this.Description = description;
            this.IsVisible = IsVisible;
            this.IsPublic = isPublic;
            UsersInCourseId = new List<int>();
            PostsInCourseId = new List<int>();
        }

        public int CompareTo(Course? other)
        {
            if (Id > other.Id || other == null)
                return 1;
            else if (Id < other.Id)
                return -1;
            return 0;
        }
    }
}
