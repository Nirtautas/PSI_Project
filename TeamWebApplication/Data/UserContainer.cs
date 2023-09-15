using TeamWebApplication.Models;
using System.Globalization;

namespace TeamWebApplication.Data
{
<<<<<<< HEAD
    public class UserContainer : IUserContainer
    {
        private int userIdCounter;
        public readonly ICollection<User> _userList = new List<User>();//properties, reiks

        public void FetchUsers()//idet I IUserContainer;void FetchUsers();pasidomet
=======
    public class UserContainer
    {
        private static UserContainer instance = null;
        private int userIdCounter;
        public readonly ICollection<User> _userList = new List<User>();

        public static UserContainer Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserContainer();
                return instance;
            }
        }

        public void fetchUsers()
>>>>>>> ee3f1aced4d147c0facef12d85f00e914c66aeed
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

<<<<<<< HEAD
        public void WriteUsers()
=======
        public void writeUsers()
>>>>>>> ee3f1aced4d147c0facef12d85f00e914c66aeed
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/UserData.txt"))
            {
                writer.WriteLine(userIdCounter);
                foreach (var user in _userList)
                    writer.WriteLine(user.ToString());
            }
        }

<<<<<<< HEAD
        public void PrintUserList()
=======
        public void printUserList()
>>>>>>> ee3f1aced4d147c0facef12d85f00e914c66aeed
        {
            foreach (var user in _userList)
                System.Diagnostics.Debug.WriteLine(user.ToString());
        }
    }
}
