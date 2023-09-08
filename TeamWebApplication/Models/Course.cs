using System.ComponentModel.DataAnnotations;
using MyWebApplication.Models;

namespace MyWebApplication.Models
{
    //Simple course model for testing
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string UploadedFiles { get; set; }
        public bool IsVisible { get; set; }
        public Course() {
        
        }
    }
}
