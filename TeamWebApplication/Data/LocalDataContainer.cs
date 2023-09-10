using MyWebApplication.Models;
using System.Globalization;

/*
 *  TODO:
 *  Think of a better way to fetch User data
 *  (It's easy to fetch data to User class, but what about fields in child classes Student and Lecturer?)
 *  Maybe implement some data checking to prevent errors when data is incorrect or incomplete.
 */

namespace TeamWebApplication.Data
{
    public sealed class LocalDataContainer
    {
        private static LocalDataContainer instance = null;

        public static int courseIdCounter;
        public static ICollection<Course> courseList { get; set; } = new List<Course>();
        public static int userIdCounter;
        public static ICollection<User> userList { get; set; } = new List<User>();
        public static IList<int> relationData { get; set; } = new List<int>();

        private static StreamReader? reader;
        private static string? readString;
        private static string[]? splitString;
        private static bool IsDataFetched = false;

        //Singleton design pattern to prevent 2 or more local databases to be created
        public static LocalDataContainer Instance
        {
            get
            {
                if (instance == null)
                    instance = new LocalDataContainer();
                return instance;
            }
        }

        public void fetchLocalData()
        {
            fetchRelationData();
            fetchCourses();
            fetchUsers();
            applyRelationData();
            IsDataFetched = true;
        }

        public void printLocalData()
        {
            if (IsDataFetched)
            {
                printCourseList();
                System.Diagnostics.Debug.WriteLine("");
                printUserList();
                System.Diagnostics.Debug.WriteLine("");
                printRelationData();
            }
        }

        private void fetchCourses()
        {
            reader = new StreamReader("./TextData/CourseData.txt");
            if ((readString = reader.ReadLine()) != null)
                courseIdCounter = Int32.Parse(readString);

            while ((readString = reader.ReadLine()) != null) {
                splitString = readString.Split(';');
                Course course = new Course(
                    Int32.Parse(splitString[0]),                                                              //id
                    splitString[1],                                                                           //name
                    DateTime.ParseExact(splitString[2], "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture), //creationDate
                    splitString[3],                                                                           //description
                    Boolean.Parse(splitString[4])                                                             //isVisible
                );
                courseList.Add(course);
            }
        }

        private void fetchUsers()
        {
            reader = new StreamReader("./TextData/UserData.txt");
            if ((readString = reader.ReadLine()) != null)
                userIdCounter = Int32.Parse(readString);

            while ((readString = reader.ReadLine()) != null) {
                splitString = readString.Split(';');
                switch (splitString[6]) {
                    case "Student":
                        Student student = new Student(
                            Int32.Parse(splitString[0]),                                                     //UserId
                            splitString[1],                                                                  //Name
                            splitString[2],                                                                  //Surname
                            DateTime.ParseExact(splitString[3], "dd-MM-yyyy", CultureInfo.InvariantCulture), //Birth Date
                            splitString[4],                                                                  //Email
                            splitString[5],                                                                  //Password
                            (Role)Enum.Parse(typeof(Role), splitString[6]),                                  //Role
                            (Faculty)Enum.Parse(typeof(Faculty), splitString[7]),                            //Faculty
                            (Specialization)Enum.Parse(typeof(Specialization), splitString[8]),              //Specialization
                            (AcademicDegree)Enum.Parse(typeof(AcademicDegree), splitString[9]),              //Academic Degree
                            Int32.Parse(splitString[10])                                                     //YearIn
                        );
                        userList.Add(student);
                        break;
                    case "Lecturer":
                        Lecturer lecturer = new Lecturer(
                            Int32.Parse(splitString[0]),                                                     //UserId
                            splitString[1],                                                                  //Name
                            splitString[2],                                                                  //Surname
                            DateTime.ParseExact(splitString[3], "dd-MM-yyyy", CultureInfo.InvariantCulture), //Birth Date
                            splitString[4],                                                                  //Email
                            splitString[5],                                                                  //Password
                            (Role)Enum.Parse(typeof(Role), splitString[6]),                                  //Role
                            (Faculty)Enum.Parse(typeof(Faculty), splitString[7]),                            //Faculty
                            (Specialization)Enum.Parse(typeof(Specialization), splitString[8]),              //Specialization
                            (Title)Enum.Parse(typeof(Title), splitString[9])                                 //Title
                        );    
                        userList.Add(lecturer);
                        break;
                }
            }
        }

        private void fetchRelationData()
        {
            reader = new StreamReader("./TextData/UserCourseRelation.txt");
            //Could use format without ';' but then data would be hard to read
            while ((readString = reader.ReadLine()) != null)
            {
                splitString = readString.Split(';');
                relationData.Add(Int32.Parse(splitString[0]));
                relationData.Add(Int32.Parse(splitString[1]));
            }
        }

        private void applyRelationData()
        {
            foreach (var course in courseList) {
                for (int x = 0 ; x < (relationData.Count() / 2) ; ++x)
                {
                    if (relationData[x * 2] == course.Id) {
                        course.UsersInCourseId.Add(relationData[x * 2 + 1]);
                        foreach (var user in userList)
                        {
                            if (relationData[x * 2 + 1] == user.UserId)
                            {
                                user.CoursesUserTakesId.Add(relationData[x * 2]);
                            }
                        }
                    }
                        
                }
            }
        }



        public void printRelationData()
        {
            for (int x = 0 ; x < (relationData.Count() / 2) ; ++x)
                System.Diagnostics.Debug.WriteLine(relationData[x * 2] + " ; " + relationData[x * 2 + 1]);
        }

        public void printCourse(Course course)
        {
            System.Diagnostics.Debug.WriteLine(
                    course.Id + " / " +
                    course.Name + " / " +
                    course.CreationDate.ToString() + " / " +
                    course.Description + " / " +
                    course.IsVisible.ToString()
                );
        }

        public void printCourseList()
        {
            foreach (var course in courseList)
                printCourse(course);
        }

        public void printUserList()
        {
            printLecturerList();
            System.Diagnostics.Debug.WriteLine("");
            printStudentList();
        }

        public void printStudent(Student student)
        {
            System.Diagnostics.Debug.WriteLine(
                student.UserId + " / " +
                student.Name + " / " +
                student.Surname + " / " +
                student.BirthDate.ToString() + " / " +
                student.Email + " / " +
                student.Password + " / " +
                student.Role.ToString() + " / " +
                student.Faculty.ToString() + " / " +
                student.Specialization.ToString() + " / " +
                student.AcademicDegree.ToString() + " / " +
                student.YearIn
            );
        }

        public void printStudentList()
        {
            foreach (Student student in userList.OfType<Student>())
                printStudent(student);
        }

        public void printLecturer(Lecturer lecturer)
        {
            System.Diagnostics.Debug.WriteLine(
                lecturer.UserId + " / " +
                lecturer.Name + " / " +
                lecturer.Surname + " / " +
                lecturer.BirthDate.ToString() + " / " +
                lecturer.Email + " / " +
                lecturer.Password + " / " +
                lecturer.Role.ToString() + " / " +
                lecturer.Faculty.ToString() + " / " +
                lecturer.Specialization.ToString() + " / " +
                lecturer.Title.ToString()
            );
        }

        public void printLecturerList()
        {
            foreach (Lecturer lecturer in userList.OfType<Lecturer>())
                printLecturer(lecturer);
        }

        public void printRelationalList()
        {
            foreach (var course in courseList)
            {
                printCourse(course);
                for (int x = 0; x < course.UsersInCourseId.Count(); ++x)
                {
                    foreach (var user in userList)
                    {
                        if (course.UsersInCourseId[x] == user.UserId)
                        {
                            System.Diagnostics.Debug.WriteLine( "\t" +
                                user.UserId + " / " +
                                user.Name + " / " +
                                user.Surname + " / " +
                                user.BirthDate.ToString() + " / " +
                                user.Email + " / " +
                                user.Password + " / " +
                                user.Role.ToString() + " / " +
                                user.Faculty.ToString() + " / " +
                                user.Specialization.ToString()
                            );
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }
    }
}
