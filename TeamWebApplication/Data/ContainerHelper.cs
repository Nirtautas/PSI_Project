namespace TeamWebApplication.Data
{
    public static class ContainerHelper
    {
        public static void fetchLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            relationContainer.fetchRelationData();
            courseContainer.fetchCourses();
            userContainer.fetchUsers();
            relationContainer.applyRelationData(courseContainer._courseList, userContainer._userList);
        }

        public static void writeLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            courseContainer.writeCourses();
            userContainer.writeUsers();
            relationContainer.writeRelationData();
        }

        public static void printLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            courseContainer.printCourseList();
            System.Diagnostics.Debug.WriteLine("");
            userContainer.printUserList();
            System.Diagnostics.Debug.WriteLine("");
            relationContainer.printRelationData();
        }

        public static void printRelationalList(CourseContainer courseContainer, UserContainer userContainer)
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
