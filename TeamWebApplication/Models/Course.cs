using System.ComponentModel.DataAnnotations;

namespace TeamWebApplication.Models
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
