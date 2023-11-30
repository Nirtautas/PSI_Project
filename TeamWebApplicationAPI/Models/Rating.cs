using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWebApplicationAPI.Models
{
    [Table("Ratings")]
    public class Rating : IComparable<Rating>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        //Increments of 0.5. Min: 0, Max: 5 stars
        private decimal userRating;
        public decimal UserRating {
            get
            {
                return userRating;
            }
            set
            {
                if (value >= 5m)
                    userRating = 5m;
                else if (value <= 0m)
                    userRating = 0m;
                else
                    userRating = Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2;
            }
        }

        //Database links
        public User User { get; set; }
        public Course Course { get; set; }

        public Rating(int userId, int courseId, decimal userRating = 5m)
        {
            UserId = userId;
            CourseId = courseId;
            UserRating = userRating;
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
