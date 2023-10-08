using TeamWebApplication.Models;

namespace TeamWebApplication.ExtensionMethods
{
    public static class CourseExtension
    {
        public static string FormattedToString(this Course course)
        {
            return
            course.Id.ToString() + ";" +
            course.Name + ";" +
            course.CreationDate.ToString() + ";" +
            course.Description + ";" +
            course.IsVisible + ";" +
            course.IsPublic;
        }
    }
}