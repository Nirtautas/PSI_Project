using System.Reflection;
using Castle.DynamicProxy;

namespace TeamWebApplicationAPI.Repositories.Interceptors
{
    public class NullCheckInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            ParameterInfo[] parameters = invocation.Method.GetParameters();

            for (int i = 0; i < parameters.Length; i++)//checking each parameter for null
            {
                object parameterValue = invocation.Arguments[i];

                if (parameterValue == null)
                {
                    var parameterName = parameters[i].Name;
                    throw new ArgumentNullException(parameterName);
                }
            }
            invocation.Proceed();//invoking original method
        }
    }
}