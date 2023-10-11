using TeamWebApplication.Models;

namespace TeamWebApplication.Data
{
    public interface ICourseContainer
    {
        void FetchCourses(IRelationContainer relationContainer);
        Course? GetCourse(int courseId);
        void PrintCourseList();
        void WriteCourses();
        int CreateCourse(Course course, int loggedInUserId);
        int DeleteCourse(Course courseToRemove);
        void PrintRelation();
        ICollection<Course> CourseList { get; }
    }
}
