using TeamWebApplication.Data.Exceptions;

namespace TeamWebApplication.Data.ExtensionMethods
{
    public static class SessionExtension
    {
        public static int GetInt32Ex(this ISession session, string key)
        {
            int? sessionResult = session.GetInt32(key);
			if (sessionResult == null)
                throw new SessionCredentialException();
            return (int)sessionResult;
        }

        public static string GetStringEx(this ISession session, string key)
        {
            string? sessionResult = session.GetString(key);
            if (sessionResult == null)
                throw new SessionCredentialException();
            return (string)sessionResult;
        }
    }
}
