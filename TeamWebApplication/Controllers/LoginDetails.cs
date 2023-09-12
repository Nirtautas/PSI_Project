using Microsoft.Build.Framework;

namespace TeamWebApplication.Models
{
    public class LoginDetails
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Password { get; set; }
        public LoginDetails()
        {

        }
    }
}
