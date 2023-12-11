using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interceptors.Interfaces;

namespace TeamWebApplicationAPI.Repositories.Interceptors
{
    public class UpdateNeededInterceptor : IUpdateNeededInterceptor
    {
        public bool IsCommentUpdateNeeded(Comment? original, string? newValue)
        {
            if (original.UserComment != newValue)
            {
                return true;
            }
            return false;
        }
    }
}
