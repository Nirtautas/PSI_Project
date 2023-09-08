using System.ComponentModel.DataAnnotations;
using MyWebApplication.Models;

namespace MyWebApplication.Models
{
    public abstract class User
    {
        //These variables are fetched from files
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Faculty Faculty { get; set; }
        public Specialization Specialization { get; set; }
        //These variables are not yet implemented
        public IEnumerable<Course> CoursesUserTakes { get; set; }
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
        MathematicsAndInformatics,
        ChemistryAndGeosciences,
        Physics,
        Filology
    }
    public enum Specialization
    {
        ProgramSystems,
        Informatics,
        Chemistry,
        Geology,
        QuantumPhysics,
        FluidPhysics,
        EnglishFilology,
        LithuanianFilology,
    }
}
