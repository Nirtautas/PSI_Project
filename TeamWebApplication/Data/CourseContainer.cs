using MyWebApplication.Models;
using System.Globalization;

namespace TeamWebApplication.Data
{
    public sealed class CourseContainer
    {
        private static CourseContainer instance = null;
        private int courseIdCounter;
        public readonly ICollection<Course> _courseList = new List<Course>();

        public static CourseContainer Instance
        {
            get
            {
                if (instance == null)
                    instance = new CourseContainer();
                return instance;
            }
        }

        public void fetchCourses()
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
                    _courseList.Add(course);
                }
            }
        }

        public void writeCourses()
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/CourseData.txt"))
            {
                writer.WriteLine(courseIdCounter);
                foreach (var course in _courseList)
                    writer.WriteLine(course.ToString());
            }
        }

        public void printCourseList()
        {
            foreach (var course in _courseList)
                System.Diagnostics.Debug.WriteLine(course.ToString());
        }
    }
}
