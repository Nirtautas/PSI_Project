using TeamWebApplication.Models;
namespace TeamWebApplication.Data
{
    public interface IRelationContainer
    {
        void FetchRelationData();
        void PrintRelationData();
        void WriteRelationData();
        public void AddRelationData(int courseId, int userId);
        ICollection<Relation> relationData { get; }
    }

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

    public class RelationContainer : IRelationContainer
    {
        public ICollection<Relation> relationData { get; }

        public RelationContainer()
        {
            relationData = new List<Relation>();
            FetchRelationData();
        }

        public void FetchRelationData()
        {
            string? readString;
            string[]? splitString;

            using (StreamReader? reader = new StreamReader("./TextData/UserCourseRelation.txt"))
            {
                //Could use format without ';' but then data would be hard to read
                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    relationData.Add(new Relation(Int32.Parse(splitString[0]), Int32.Parse(splitString[1])));
                }
            }
        }

        public void AddRelationData(int courseId, int userId)
        {
            relationData.Add(new Relation(courseId, userId));
            WriteRelationData();
        }

        public void WriteRelationData()
        {
            using (StreamWriter? writer = new StreamWriter("./TextData/UserCourseRelation.txt"))
            {
                foreach (var relation in relationData)
                    writer.WriteLine(relation.courseId + ";" + relation.userId);
            }
        }

        public void PrintRelationData()
        {
            foreach (var relation in relationData)
                System.Diagnostics.Debug.WriteLine(relation.courseId + " ; " + relation.userId);
        }
    }
}
