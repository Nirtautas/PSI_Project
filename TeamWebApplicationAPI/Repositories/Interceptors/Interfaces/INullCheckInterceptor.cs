using TeamWebApplicationAPI.Models;

namespace TeamWebApplicationAPI.Repositories.Interceptors.Interfaces
{
    public interface INullCheckInterceptor
    {
        void CheckId(int? Id);
        void CheckString(string? input);
        void CheckForNullValues(Comment? comment);
    }
}
