using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebApplication.Models
{
	[Table("CourseUsers")]
	public class CourseUser
	{
		public int CourseId { get; set; }
		public int UserId { get; set; }

		//Database links
		public Course Course { get; set; }
		public User User { get; set; }
	}
}
