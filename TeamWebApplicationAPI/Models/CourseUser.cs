using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TeamWebApplicationAPI.Models
{
	[Table("CourseUsers")]
	public class CourseUser
	{
		public int CourseId { get; set; }
		public int UserId { get; set; }

        //Database links
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
	}
}
