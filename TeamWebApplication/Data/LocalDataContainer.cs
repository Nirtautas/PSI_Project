<<<<<<< HEAD
﻿using MyWebApplication.Models;
=======
﻿using TeamWebApplication.Models;
>>>>>>> Brigita
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
        //Relation data is held in one list in format [0] Course, [1] User, [2] Course, ...
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
            //For each course in courseList
            foreach (var course in courseList) {
                //Go through course id's in relationData
                for (int x = 0 ; x < (relationData.Count() / 2) ; ++x)
                {
                    //If course id == to course id's in relationData
                    if (relationData[x * 2] == course.Id) {
                        //Add user id from relationData to course
                        course.UsersInCourseId.Add(relationData[x * 2 + 1]);
                        //Then for each user in userList
                        foreach (var user in userList)
                        {
                            //Check if user id == to course id
                            if (relationData[x * 2 + 1] == user.UserId)
                                user.CoursesUserTakesId.Add(relationData[x * 2]);
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

        public void printCourseList()
        {
            foreach (var course in courseList)
                System.Diagnostics.Debug.WriteLine(course.ToString());
        }

        public void printUserList()
        {
            foreach(var user in userList)
                System.Diagnostics.Debug.WriteLine(user.ToString());
        }

        public void printRelationalList()
        {
            foreach (var course in courseList)
            {
                System.Diagnostics.Debug.WriteLine(course.ToString());
                for (int x = 0; x < course.UsersInCourseId.Count(); ++x)
                {
                    foreach (var user in userList)
                    {
                        if (course.UsersInCourseId[x] == user.UserId)
                            System.Diagnostics.Debug.WriteLine("\t" + user.ToString());
                    }
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }
    }
}
