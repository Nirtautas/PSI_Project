using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamWebApplication.Models;

namespace TeamWebApplication.Models
{
    public class User : IComparable<User>
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Faculty Faculty { get; set; }
        public Specialization Specialization { get; set; }

        //Database links
        public ICollection<CourseUser> Courses { get; set; }
        public ICollection<Comment> Comments { get; set; }

        //Unmapped fields
        [NotMapped]
        public ICollection<int>? CoursesUserTakesId { get; set; }

        public User()
        {
            CoursesUserTakesId = new List<int>();
        }

        public User(int id, string name, string surname, string email, string password, Role role, Faculty faculty, Specialization specialization)
        {
            this.UserId = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.Faculty = faculty;
            this.Specialization = specialization;
            CoursesUserTakesId = new List<int>();
        }

        public override string ToString()
        {
            return
                UserId.ToString() + ";" +
                Name + ";" +
                Surname + ";" +
                Email + ";" +
                Password + ";" +
                Role.ToString() + ";" +
                Faculty.ToString() + ";" +
                Specialization.ToString();
        }

        public int CompareTo(User? other)
        {
            if (UserId > other.UserId || other == null)
                return 1;
            else if (UserId < other.UserId)
                return -1;
            return 0;
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
        [Display(Name = "Chemistry and Geosciences")]
        ChemistryAndGeosciences,
        Physics,
        Filology
    }
    public enum Specialization
    {
        [Display(Name = "Program systems")]
        ProgramSystems,
        Informatics,
        Chemistry,
        Geology,
        [Display(Name = "Quantum physics")]
        QuantumPhysics,
        [Display(Name = "Fluid physics")]
        FluidPhysics,
        [Display(Name = "English filology")]
        EnglishFilology,
        [Display(Name = "Lithuanian filology")]
        LithuanianFilology,
    }
}
