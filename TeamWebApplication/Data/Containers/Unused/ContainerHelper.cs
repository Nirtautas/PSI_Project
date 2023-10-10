using TeamWebApplication.ExtensionMethods;

namespace TeamWebApplication.Data.Containers.Unused
{
    public static class ContainerHelper
    {

        public static void WriteLocalData(IRelationContainer relationContainer, ICourseContainer courseContainer, IUserContainer userContainer)
        {
            courseContainer.WriteCourses();
            userContainer.WriteUsers();
            relationContainer.WriteRelationData();
        }

        public static void PrintLocalData(IRelationContainer relationContainer, ICourseContainer courseContainer, IUserContainer userContainer)
        {
            courseContainer.PrintCourseList();
            System.Diagnostics.Debug.WriteLine("");
            userContainer.PrintUserList();
            System.Diagnostics.Debug.WriteLine("");
            relationContainer.PrintRelationData();
        }

        public static void PrintRelationalList(ICourseContainer courseContainer, IUserContainer userContainer)
        {
            foreach (var course in courseContainer.courseList)
            {
                System.Diagnostics.Debug.WriteLine(course.FormattedToString());
                foreach (var inUser in course.UsersInCourseId)
                {
                    foreach (var user in userContainer.userList)
                    {
                        if (inUser == user.UserId)
                            System.Diagnostics.Debug.WriteLine("\t" + user.FormattedToString());
                    }
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }
    }
}
