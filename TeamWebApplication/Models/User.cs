using System.ComponentModel.DataAnnotations;

namespace TeamWebApplication.Models
{
    public class User : IComparable<User>
    {
        //These variables are fetched from files
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        // BirthDate not implemented yet in the frontend
        // public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Faculty Faculty { get; set; }
        public Specialization Specialization { get; set; }
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

        public int CompareTo(User? other)
        {//User su esamu User, ids; td sort ICollection - tik realizacija, var list.. add sort
            throw new NotImplementedException();
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
