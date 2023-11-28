using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebApplication.Models
{
    [Table("Ratings")]
    public class Rating : IComparable<Rating>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        private int userRating;
        public int UserRating {
            get
            {
                return userRating;
            }
            set
            {
                if (value > 5)
                    userRating = 5;
                if (value < 1)
                    userRating = 1;
                userRating = value;
            }
        }

        //Database links
        public User User { get; set; }
        public Course Course { get; set; }

        public Rating(int userId, int courseId, int userRating = 5)
        {
            UserId = userId;
            CourseId = courseId;
            this.userRating = userRating;
        }

        public int CompareTo(Rating? other)
        {
            if (userRating > other.userRating || other == null)
                return 1;
            else if (userRating < other.userRating)
                return -1;
            return 0;
        }
    }
}
