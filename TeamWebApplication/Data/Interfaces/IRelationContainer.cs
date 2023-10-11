namespace TeamWebApplication.Data
{
    public interface IRelationContainer
    {
        void FetchRelationData();
        void PrintRelationData();
        void WriteRelationData();
        void AddRelationData(int courseId, int userId);
        void RemoveRelationData(int courseId, int userId);
        void DeleteRelationData(int courseId, int userId);
        ICollection<Relation<int>> RelationData { get; }
    }
}
