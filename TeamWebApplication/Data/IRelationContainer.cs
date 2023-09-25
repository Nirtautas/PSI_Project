namespace TeamWebApplication.Data
{
    public interface IRelationContainer
    {
        void FetchRelationData();
        void PrintRelationData();
        void WriteRelationData();
        public void AddRelationData(int courseId, int userId);
        public void DeleteRelationData(int courseId, int userId);
        ICollection<Relation> relationData { get; }
    }
}
