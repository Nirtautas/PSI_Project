using TeamWebApplicationAPI.Models;

namespace TeamWebApplicationAPI.Repositories.Interceptors.Interfaces
{
    public interface IUpdateNeededInterceptor
    {
        bool IsCommentUpdateNeeded(Comment? original, string? newValue);
    }
}
