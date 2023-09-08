using Microsoft.AspNetCore.Identity;
using MyWebApplication.Models;

namespace TeamWebApplication.Data
{
    public static class LocalDataContainer
    {
        public static ICollection<Course> CourseList { get; set; } = new List<Course>();
        public static ICollection<User> UserList { get; set; } = new List<User>();

        private static StreamReader? reader;
        private static string? readString;
        private static string[]? splitString;

        public static void fetchLocalData() {
            fetchCourses();
            fetchUsers();
        }

        public static void printLocalData()
        {
            printCourseList();
            System.Diagnostics.Debug.WriteLine("");
            printUserList();
        }

        public static void fetchCourses() {
            reader = new StreamReader("./TextData/CourseData.txt");
            while ((readString = reader.ReadLine()) != null) {
                splitString = readString.Split(';');
                Course course = new Course(
                    Int32.Parse(splitString[0]),    //id
                    splitString[1],                 //name
                    DateTime.Parse(splitString[2]), //creationDate
                    splitString[3],                 //description
                    Boolean.Parse(splitString[4])   //isVisible
                );
                CourseList.Add(course);
            }
        }

        public static void fetchUsers() {
            reader = new StreamReader("./TextData/UserData.txt");
            while ((readString = reader.ReadLine()) != null) {
                splitString = readString.Split(';');
                switch (splitString[6]) {
                    case "Student":
                        Student student = new Student(
                            Int32.Parse(splitString[0]),                                            //UserId
                            splitString[1],                                                         //Name
                            splitString[2],                                                         //Surname
                            DateTime.Parse(splitString[3]),                                         //Birth Date
                            splitString[4],                                                         //Email
                            splitString[5],                                                         //Password
                            (Role)Enum.Parse(typeof(Role), splitString[6]),                         //Role
                            (Faculty)Enum.Parse(typeof(Faculty), splitString[7]),                   //Faculty
                            (Specialization)Enum.Parse(typeof(Specialization), splitString[8]),     //Specialization
                            (AcademicDegree)Enum.Parse(typeof(AcademicDegree), splitString[9]),     //Academic Degree
                            Int32.Parse(splitString[10])                                            //YearIn
                        );
                        UserList.Add(student);
                        break;
                    case "Lecturer":
                        Lecturer lecturer = new Lecturer(
                            Int32.Parse(splitString[0]),                                            //UserId
                            splitString[1],                                                         //Name
                            splitString[2],                                                         //Surname
                            DateTime.Parse(splitString[3]),                                         //Birth Date
                            splitString[4],                                                         //Email
                            splitString[5],                                                         //Password
                            (Role)Enum.Parse(typeof(Role), splitString[6]),                         //Role
                            (Faculty)Enum.Parse(typeof(Faculty), splitString[7]),                   //Faculty
                            (Specialization)Enum.Parse(typeof(Specialization), splitString[8]),     //Specialization
                            (Title)Enum.Parse(typeof(Title), splitString[9])                        //Title
                        );    
                        UserList.Add(lecturer);
                        break;
                }
            }
        }

        public static void printCourseList() {
            foreach (var course in CourseList)
            {
                System.Diagnostics.Debug.WriteLine(
                    course.Id + " / " +
                    course.Name + " / " +
                    course.CreationDate.ToString() + " / " +
                    course.Description + " / " +
                    course.IsVisible.ToString()
                );
            }
        }

        public static void printUserList()
        {
            foreach (Lecturer lecturer in UserList.OfType<Lecturer>())
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
            System.Diagnostics.Debug.WriteLine("");
            foreach (Student student in UserList.OfType<Student>())
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
        }
    }
}
