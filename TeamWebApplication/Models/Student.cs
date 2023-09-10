using MyWebApplication.Models;

namespace MyWebApplication.Models
{
    public class Student : User
    {
        //These variables are fetched from files
        public AcademicDegree AcademicDegree { get; set; }
        public int YearIn { get; set; }
        //These variables are not yet implemented
        public List<string> UploadedFiles { get; set; }
        
        public Student()
        {

        }

        public Student(
            int userId,
            string name,
            string surname,
            DateTime birthDate,
            string email,
            string password,
            Role role,
            Faculty faculty,
            Specialization specialization,
            AcademicDegree academicDegree,
            int yearIn)
        {
            UserId = userId;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Email = email;
            Password = password;
            Role = role;
            Faculty = faculty;
            Specialization = specialization;
            AcademicDegree = academicDegree;
            YearIn = yearIn;
            CoursesUserTakesId = new List<int>();
        }
    }
    public enum AcademicDegree
    {
        Bachelor,
        Master,
        Doctorate
    }

}
