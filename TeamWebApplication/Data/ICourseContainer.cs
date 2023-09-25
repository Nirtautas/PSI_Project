using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface ICourseContainer
    {
        void FetchCourses(IRelationContainer relationContainer);
        void PrintCourseList();
        void WriteCourses();
        int CreateCourse(Course course, int loggedInUserId);
        public void PrintRelation();
        ICollection<Course> courseList { get; }
        public int currentCourseId { get; set; }
    }
}
