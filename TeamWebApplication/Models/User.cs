using System.ComponentModel.DataAnnotations;
using MyWebApplication.Models;

namespace MyWebApplication.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public Faculty Faculty { get; set; }
        public Specialization Specialization { get; set; }

        public IEnumerable<Course> CourseUsers { get; set; }
        public User()
        {

        }

    }
    public enum Role
    {
        Student,
        Lecturer
    }
    public enum Faculty
    {
        Filology,
        MathematicsAndInformatics,
        Medicine
    }
    public enum Specialization
    {
        ProgramSystems,
        Informatics
    }
}
