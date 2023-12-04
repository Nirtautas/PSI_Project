using TeamWebApplicationAPI.Models;

namespace TeamWebApplication.Data.ExtensionMethods
{
    public static class CourseExtension
    {
        public static string FormattedToString(this Course course)
        {
            return
            course.CourseId.ToString() + ";" +
            course.Name + ";" +
            course.CreationDate.ToString() + ";" +
            course.Description + ";" +
            course.IsVisible + ";" +
            course.IsPublic;
        }
    }
}