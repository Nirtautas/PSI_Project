using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamWebApplication.Models.Enums;

namespace TeamWebApplication.Models
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
        public ICollection<CourseUser> Courses { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public User()
        {

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
