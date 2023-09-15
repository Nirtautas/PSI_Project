using TeamWebApplication.Models;
using System.Globalization;

namespace TeamWebApplication.Data
{
    public class UserContainer : IUserContainer
    {
        private static UserContainer instance = null;
        private int userIdCounter;
        public readonly ICollection<User> _userList = new List<User>();//properties, reiks

        public UserContainer FetchUsers(UserContainer instance)//idet I IUserContainer;void FetchUsers()
        {
            if (instance == null)
                instance = new UserContainer();
            return instance;
        }


        public void FetchUsers()
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
                            _userList.Add(student);
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
                            _userList.Add(lecturer);
                            break;
                    }
                }
            }
        }

        public void WriteUsers()
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/UserData.txt"))
            {
                writer.WriteLine(userIdCounter);
                foreach (var user in _userList)
                    writer.WriteLine(user.ToString());
            }
        }

        public void PrintUserList()
        {
            foreach (var user in _userList)
                System.Diagnostics.Debug.WriteLine(user.ToString());
        }
    }
}