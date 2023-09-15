using TeamWebApplication.Models;
namespace TeamWebApplication.Data
{
    public struct Relation
    {
        public int courseId;
        public int userId;

        public Relation(int courseId, int userId)
        {
            this.courseId = courseId;
            this.userId = userId;
        }
    }

    public class RelationContainer
    {
        private static RelationContainer instance = null;
        public readonly ICollection<Relation> _relationData = new List<Relation>();

        public static RelationContainer Instance
        {
            get
            {
                if (instance == null)
                    instance = new RelationContainer();
                return instance;
            }
        }

<<<<<<< HEAD
        public void fetchRelationData()
=======
        public void FetchRelationData()
>>>>>>> main
        {
            string? readString;
            string[]? splitString;

            using (StreamReader? reader = new StreamReader("./TextData/UserCourseRelation.txt"))
            {
                //Could use format without ';' but then data would be hard to read
                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    _relationData.Add(new Relation(Int32.Parse(splitString[0]), Int32.Parse(splitString[1])));
                }
            }
        }

<<<<<<< HEAD
        public void writeRelationData()
=======
        public void WriteRelationData()
>>>>>>> main
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/UserCourseRelation.txt"))
            {
                foreach (var relation in _relationData)
                    writer.WriteLine(relation.courseId + ";" + relation.userId);
            }
        }

<<<<<<< HEAD
        public void applyRelationData(ICollection<Course> courseList, ICollection<User> userList)
=======
        public void ApplyRelationData(ICollection<Course> courseList, ICollection<User> userList)
>>>>>>> main
        {
            foreach (Relation relation in _relationData)
            {
                foreach (var course in courseList)
                {
                    if (relation.courseId == course.Id)
                    {
                        course.UsersInCourseId.Add(relation.userId);
                        foreach (var user in userList)
                        {
                            if (relation.userId == user.UserId)
                               user.CoursesUserTakesId.Add(relation.courseId);
                        }
                    }
                }
            }
        }

<<<<<<< HEAD
        public void printRelationData()
=======
        public void PrintRelationData()
>>>>>>> main
        {
            foreach (var relation in _relationData)
                System.Diagnostics.Debug.WriteLine(relation.courseId + " ; " + relation.userId);
        }
    }
}
