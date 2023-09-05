using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Models
{
    //Simple course model for testing
    public class Course
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public Course() {
        
        }
    }
}
