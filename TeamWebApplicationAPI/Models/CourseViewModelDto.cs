namespace TeamWebApplicationAPI.Models
{
    public class CourseViewModelDto
    {
        public IEnumerable<CourseDto> Courses { get; set; }
        public UserDto User { get; set; }
    }
}
