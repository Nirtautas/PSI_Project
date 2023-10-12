using System.ComponentModel.DataAnnotations;

namespace TeamWebApplication.Models
{
	public class CourseUser
	{
		public int CourseId { get; set; }
		public int UserId { get; set; }

		//Database links
		public Course Course { get; set; }
		public User User { get; set; }
	}
}
