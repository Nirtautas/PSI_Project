using TeamWebApplicationAPI.Models;

namespace TeamWebApplicationAPI.Repositories.Interceptors.Interfaces
{
    public interface INullCheckInterceptor<T>
    {
        void CheckIfNotNull<T>(T? input);
    }
}
