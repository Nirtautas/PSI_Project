using TeamWebApplication.Models;

namespace TeamWebApplication.Data.ExtensionMethods
{
    public static class UserExtension
    {
        public static string FormattedToString(this User user)
        {
            return
                user.UserId.ToString() + ";" +
                user.Name + ";" +
                user.Surname + ";" +
                user.Email + ";" +
                user.Password + ";" +
                user.Role.ToString() + ";" +
                user.Faculty.ToString() + ";" +
                user.Specialization.ToString();
        }
    }
}