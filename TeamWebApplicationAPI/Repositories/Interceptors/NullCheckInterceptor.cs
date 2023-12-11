using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Repositories.Interceptors.Interfaces;

namespace TeamWebApplicationAPI.Repositories.Interceptors
{

    public class NullCheckInterceptor : INullCheckInterceptor
    {
        public void CheckId(int? Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }
        }
        public void CheckString(string? input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
        }
        public void CheckForNullValues(Comment? comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }
        }
    }
}
