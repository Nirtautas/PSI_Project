using System.ComponentModel.DataAnnotations;
using TeamWebApplicationAPI.Models.Enums;

namespace TeamWebApplicationAPI.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Faculty Faculty { get; set; }
        public Specialization Specialization { get; set; }
    }
}
