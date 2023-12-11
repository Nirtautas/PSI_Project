using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interceptors.Interfaces;

namespace TeamWebApplicationAPI.Repositories.Interceptors
{

    public class NullCheckInterceptor<T>: INullCheckInterceptor<T>
    {
        public void CheckIfNotNull<T>(T? input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
        }
    }
}
