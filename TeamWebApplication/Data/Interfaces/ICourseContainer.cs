using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface ICourseContainer
    {
        void FetchCourses(IRelationContainer relationContainer);
        public Course? GetCourse(int courseId);
        void PrintCourseList();
        void WriteCourses();
        int CreateCourse(Course course, int loggedInUserId);
        public int DeleteCourse(Course courseToRemove);
        public void PrintRelation();
        ICollection<Course> courseList { get; }
    }
}
