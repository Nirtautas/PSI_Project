using TeamWebApplication.Models;
using System.Globalization;

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

    public sealed class CourseContainer : ICourseContainer
    {
        public int currentCourseId { get; set; } = 0;
        private int courseIdCounter;
        public ICollection<Course> courseList { get; }

        public CourseContainer(IRelationContainer relationContainer)
        {
            courseList = new List<Course>();
            FetchCourses(relationContainer);
        }


		public void FetchCourses(IRelationContainer relationContainer)
        {
            string? readString;
            string[]? splitString;
            using (StreamReader? reader = new StreamReader("./TextData/CourseData.txt"))
            {
                if ((readString = reader.ReadLine()) != null)
                    courseIdCounter = Int32.Parse(readString);

                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    Course course = new Course(
                        Int32.Parse(splitString[0]),                                                              //id
                        splitString[1],                                                                           //name
                        DateTime.ParseExact(splitString[2], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), //creationDate
                        splitString[3],                                                                           //description
                        Boolean.Parse(splitString[4])                                                             //isVisible
                    );
                    foreach (Relation relation in relationContainer.relationData)
                    {
                        if (Int32.Parse(splitString[0]) == relation.courseId)
                            course.UsersInCourseId.Add(relation.userId);
                    }
                    courseList.Add(course);
                }
            }
        }

        public int CreateCourse(Course course, int loggedInUserId)
        {
            course.Id = courseIdCounter;
            courseIdCounter++;
            course.CreationDate = DateTime.Now;
            course.UsersInCourseId.Add(loggedInUserId);
            courseList.Add(course);
            WriteCourses();
            return course.Id;
        }

        public void WriteCourses()
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/CourseData.txt"))
            {
                writer.WriteLine(courseIdCounter);
                foreach (var course in courseList)
                    writer.WriteLine(course.ToString());
            }
        }

        public void PrintCourseList()
        {
            foreach (var course in courseList)
                System.Diagnostics.Debug.WriteLine(course.ToString());
        }

        public void PrintRelation()
        {
            foreach (var course in courseList)
            {
                foreach (var relation in course.UsersInCourseId)
                    System.Diagnostics.Debug.WriteLine(relation);
            }
        }
    }
}
