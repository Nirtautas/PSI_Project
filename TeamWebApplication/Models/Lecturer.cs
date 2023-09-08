using MyWebApplication.Models;

namespace MyWebApplication.Models
{
    public class Lecturer : User
    {
        //These variables are fetched from files
        public Title Title { get; set; }
        public Lecturer() {
        
        }

        public Lecturer(
            int userId,
            string name,
            string surname,
            DateTime birthDate,
            string email,
            string password,
            Role role,
            Faculty faculty,
            Specialization specialization,
            Title title)
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
            Title = title;
        }
    }
    public enum Title
    {
        Professor,
        AssistantProfessor,
        AssociateProfessor,
        Lecturer
    }
}
