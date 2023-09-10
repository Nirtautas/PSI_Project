using System.ComponentModel.DataAnnotations;
using TeamWebApplication.Models;

namespace TeamWebApplication.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
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

        [Display(Name = "Mathematics and Informatics")]
        MathematicsAndInformatics,

        [Display(Name = "Medicine")]
        Medicine,
    }


    public enum Specialization
    {
        ProgramSystems,
        Informatics,
        Bioinformatics,
        Pharmacy,
        Odontology,
        Medicine
    }
}
