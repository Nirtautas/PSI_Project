using System.ComponentModel.DataAnnotations;
using MyWebApplication.Models;

namespace MyWebApplication.Models
{
    public class Course
    {
        //These variables are fetched from files
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        //These variables are not yet implemented
        public string UploadedFiles { get; set; }
        public IEnumerable<User> UsersInCourse { get; set; }

        public Course() { 
        
        }
        public Course(int id, string name, DateTime creationDate, string description, bool isVisible) {
            this.Id = id;
            this.Name = name;
            this.CreationDate = creationDate;
            this.Description = description;
            this.IsVisible = isVisible;
        }
    }
}
