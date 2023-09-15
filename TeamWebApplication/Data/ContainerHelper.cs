namespace TeamWebApplication.Data
{
    public static class ContainerHelper
    {
        public static void FetchLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            relationContainer.FetchRelationData();
            courseContainer.FetchCourses();
            userContainer.FetchUsers();
            relationContainer.ApplyRelationData(courseContainer._courseList, userContainer._userList);
        }

        public static void WriteLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            courseContainer.WriteCourses();
            userContainer.WriteUsers();
            relationContainer.WriteRelationData();
        }

        public static void PrintLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            courseContainer.PrintCourseList();
            System.Diagnostics.Debug.WriteLine("");
            userContainer.PrintUserList();
            System.Diagnostics.Debug.WriteLine("");
            relationContainer.PrintRelationData();
        }

        public static void PrintRelationalList(CourseContainer courseContainer, UserContainer userContainer)
        {
            foreach (var course in courseContainer._courseList)
            {
                System.Diagnostics.Debug.WriteLine(course.ToString());
                foreach (var inUser in course.UsersInCourseId)
                {
                    foreach (var user in userContainer._userList)
                    {
                       if (inUser == user.UserId)
                           System.Diagnostics.Debug.WriteLine("\t" + user.ToString());
                    }
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }
    }
}
