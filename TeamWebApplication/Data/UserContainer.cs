using TeamWebApplication.Models;
using System.Globalization;
using System.Collections.Generic;

namespace TeamWebApplication.Data
{
    public class UserContainer : IUserContainer
    {
		public int loggedInUserId { get; set; } = 0;
		private int userIdCounter;
        public ICollection<User> userList { get; }

        public UserContainer(IRelationContainer relationContainer)
        {
            userList = new List<User>();
            FetchUsers(relationContainer);
        }

        public void FetchUsers(IRelationContainer relationContainer)
        {
            string? readString;
            string[]? splitString;
            using (StreamReader? reader = new StreamReader("./TextData/UserData.txt"))
            {
                if ((readString = reader.ReadLine()) != null)
                    userIdCounter = Int32.Parse(readString);

                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    switch (splitString[6])
                    {
                        case "Student":
                            Student student = new Student(
                                Int32.Parse(splitString[0]),                                                     //UserId
                                splitString[1],                                                                  //Name
                                splitString[2],                                                                  //Surname
                                DateTime.ParseExact(splitString[3], "yyyy-MM-dd", CultureInfo.InvariantCulture), //Birth Date
                                splitString[4],                                                                  //Email
                                splitString[5],                                                                  //Password
                                (Role)Enum.Parse(typeof(Role), splitString[6]),                                  //Role
                                (Faculty)Enum.Parse(typeof(Faculty), splitString[7]),                            //Faculty
                                (Specialization)Enum.Parse(typeof(Specialization), splitString[8]),              //Specialization
                                (AcademicDegree)Enum.Parse(typeof(AcademicDegree), splitString[9]),              //Academic Degree
                                Int32.Parse(splitString[10])                                                     //YearIn
                            );
                            foreach (Relation relation in relationContainer.relationData)
                            {
                                if(Int32.Parse(splitString[0]) == relation.userId)
                                    student.CoursesUserTakesId.Add(relation.courseId);
                            }
                            userList.Add(student);
                            break;
                        case "Lecturer":
                            Lecturer lecturer = new Lecturer(
                                Int32.Parse(splitString[0]),                                                     //UserId
                                splitString[1],                                                                  //Name
                                splitString[2],                                                                  //Surname
                                DateTime.ParseExact(splitString[3], "yyyy-MM-dd", CultureInfo.InvariantCulture), //Birth Date
                                splitString[4],                                                                  //Email
                                splitString[5],                                                                  //Password
                                (Role)Enum.Parse(typeof(Role), splitString[6]),                                  //Role
                                (Faculty)Enum.Parse(typeof(Faculty), splitString[7]),                            //Faculty
                                (Specialization)Enum.Parse(typeof(Specialization), splitString[8]),              //Specialization
                                (Title)Enum.Parse(typeof(Title), splitString[9])                                 //Title
                            );
                            foreach (Relation relation in relationContainer.relationData)
                            {
                                if (Int32.Parse(splitString[0]) == relation.userId)
                                    lecturer.CoursesUserTakesId.Add(relation.courseId);
                            }
                            userList.Add(lecturer);
                            break;
                    }
                }
            }
        }

        public void AddRelation(int userId, int courseId)
        {
            User user = userList.SingleOrDefault(user => user.UserId == userId);
            user.CoursesUserTakesId.Add(courseId);
        }

        public void WriteUsers()
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/UserData.txt"))
            {
                writer.WriteLine(userIdCounter);
                foreach (var user in userList)
                    writer.WriteLine(user.ToString());
            }
        }
        public void PrintUserList()
        {
            foreach (var user in userList)
                System.Diagnostics.Debug.WriteLine(user.ToString());
        }
    }
}