using TeamWebApplication.Models;
using System.Globalization;

namespace TeamWebApplication.Models
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
            // BirthDate = birthDate;
            Email = email;
            Password = password;
            Role = role;
            Faculty = faculty;
            Specialization = specialization;
            Title = title;
            CoursesUserTakesId = new List<int>();
        }

        public override string ToString()
        {
            return
                UserId.ToString() + ";" +
                Name + ";" +
                Surname + ";" +
                // BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + ";" +
                Email + ";" +
                Password + ";" +
                Role.ToString() + ";" +
                Faculty.ToString() + ";" +
                Specialization.ToString() + ";" +
                Title.ToString();
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
