using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebApplication.Models
{
	[Table("UserDetails")]
	public class UserDetails
	{
		[Key]
		public int Id { get; set; }
		public int loggedInUserId { get; set; }
		public Role loggedInUserRole { get; set; }
		public int currentCourseId { get; set; }
	}
}
