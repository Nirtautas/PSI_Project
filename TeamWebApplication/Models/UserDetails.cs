using System.ComponentModel.DataAnnotations;

namespace TeamWebApplication.Models
{
	public class UserDetails
	{
		[Key]
		public int Id { get; set; }
		public int loggedInUserId { get; set; }
		public Role loggedInUserRole { get; set; }
		public int currentCourseId { get; set; }
	}
}
