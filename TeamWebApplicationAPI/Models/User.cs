using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using TeamWebApplicationAPI.Models.Enums;

namespace TeamWebApplicationAPI.Models
{
	[Table("Users")]
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
        public virtual ICollection<CourseUser> Courses { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }

        public User()
        {

        }

        public User(int id, string name = "dname", string surname = "dsurname", string email = "demail",
            string password = "dpassword", Role role = Role.None, Faculty faculty = Faculty.None,
            Specialization specialization = Specialization.None)
        {
            this.UserId = id;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Password = password;
            this.Role = role;
            this.Faculty = faculty;
            this.Specialization = specialization;
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
}
