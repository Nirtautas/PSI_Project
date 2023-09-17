using TeamWebApplication.Models;
using System.Globalization;

namespace TeamWebApplication.Data
{
    public interface ICourseContainer
    {
        void FetchCourses();
        void PrintCourseList();
        void WriteCourses();
        ICollection<Course> courseList { get; }
    }

    public sealed class CourseContainer : ICourseContainer
    {
        private int courseIdCounter;
        public ICollection<Course> courseList { get; }

        public CourseContainer()
        {
            courseList = new List<Course>();
            FetchCourses();
        }

		public void FetchCourses()
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
                    courseList.Add(course);
                }
            }
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
    }
}
