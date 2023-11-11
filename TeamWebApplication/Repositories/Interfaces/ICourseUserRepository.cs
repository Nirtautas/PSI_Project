using TeamWebApplication.Models;

namespace TeamWebApplication.Repositories.Interfaces
{
    public interface ICourseUserRepository
    {
        IEnumerable<int> GetUsersByCourseId(int courseId);
        IEnumerable<int> GetCoursesByUserId(int userId);
        void InsertRelation(int courseId, int userId);
        void DeleterRelation(int courseId, int userId);
        void Save();
    }
}
