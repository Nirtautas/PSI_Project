﻿using TeamWebApplication.Models;
namespace TeamWebApplication.Data
{
    public class RelationContainer : IRelationContainer
    {
        public string FetchingPath { get; }
        public ICollection<Relation<int>> relationData { get; }

        public RelationContainer(string fetchingPath = "./TextData/UserCourseRelation.txt")
        {
            this.FetchingPath = fetchingPath;
            relationData = new List<Relation<int>>();
            FetchRelationData();
        }

        public void FetchRelationData()
        {
            string? readString;
            string[]? splitString;

            using (StreamReader? reader = new StreamReader(FetchingPath))
            {
                while ((readString = reader.ReadLine()) != null)
                {
                    splitString = readString.Split(';');
                    relationData.Add(new Relation<int>(Int32.Parse(splitString[0]), Int32.Parse(splitString[1]))); //Course, User
                }
            }
        }

        public void AddRelationData(int courseId, int userId)
        { 
            if (!relationData.Any(item => item.Value1 == courseId && item.Value2 == userId)) //Course, User
            {
                relationData.Add(new Relation<int>(courseId, userId));
                WriteRelationData();
            }
        }

        public void RemoveRelationData(int courseId, int userId)
        {
            relationData.Remove(new Relation<int>(courseId, userId));
            WriteRelationData();
        }

        public void DeleteRelationData(int courseId, int userId)
        {
            var relationToRemove = relationData.FirstOrDefault(relation => relation.Value1 == courseId && relation.Value2 == userId); //Course, User
            relationData.Remove(relationToRemove);
            WriteRelationData();
        }

        public void WriteRelationData()
        {
            using (StreamWriter? writer = new StreamWriter(FetchingPath))
            {
                foreach (var relation in relationData)
                    writer.WriteLine(relation.ToString());
            }
        }

        public void PrintRelationData()
        {
            foreach (var relation in relationData)
                System.Diagnostics.Debug.WriteLine(relation.ToString());
        }
    }
}
