namespace TeamWebApplication.Models
{
    public class CourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public User User { get; set; }
    }
}
