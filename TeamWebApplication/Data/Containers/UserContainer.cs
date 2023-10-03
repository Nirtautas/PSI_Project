using TeamWebApplication.Models;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;

namespace TeamWebApplication.Data
{
    public class UserContainer : IUserContainer
    {
        public string FetchingPath { get; }
        public int loggedInUserId { get; set; } = 0;
        public Role? loggedInUserRole { get; set; } = null;
        public int currentCourseId { get; set; } = 0;
        private int userIdCounter;
        public ICollection<User> userList { get; }

        public UserContainer(IRelationContainer relationContainer, string fetchingPath = "./TextData/UserData.txt")
        {
            this.FetchingPath = fetchingPath;
            userList = new List<User>();
            FetchUsers(relationContainer);
        }

        public void FetchUsers(IRelationContainer relationContainer)
        {
            string? readString;
            string[]? splitString;
            using (StreamReader? reader = new StreamReader(FetchingPath))
            {
                if ((readString = reader.ReadLine()) != null)
                    userIdCounter = Int32.Parse(readString);

                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    User user = new User(
                        id: Int32.Parse(splitString[0]),                                                
                        name: splitString[1],                                                           
                        surname: splitString[2],                                                               
                        email: splitString[3],                                                           
                        password: splitString[4],                                                               
                        role: (Role)Enum.Parse(typeof(Role), splitString[5]),                              
                        faculty: (Faculty)Enum.Parse(typeof(Faculty), splitString[6]),                          
                        specialization: (Specialization)Enum.Parse(typeof(Specialization), splitString[7])          
                        );
                    foreach (Relation<int> relation in relationContainer.relationData)
                    {
                        if (Int32.Parse(splitString[0]) == relation.value2) //User
                            user.CoursesUserTakesId.Add(relation.value1); //Course
                    }
                    userList.Add(user);
                }
            }
        }

        public User? GetUser(int userId)
        {
            User? user = userList.SingleOrDefault(user => user.UserId == userId);
            return user;
        }

        public void AddRelation(int userId, int courseId)
        {
            User? user;
            if ((user = GetUser(userId)) != null)
                user.CoursesUserTakesId.Add(courseId);
        }

        public void CreateUser(User user)
        {
            user.UserId = userIdCounter;
            userIdCounter++;
            userList.Add(user);
        }

        public void DeleteRelation(int userId, int courseId)
        {
            User? user;
            if ((user = GetUser(userId)) != null)
                user.CoursesUserTakesId.Remove(courseId);
        }

        public void WriteUsers()
        {
            using (StreamWriter? writer = new StreamWriter(FetchingPath))
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