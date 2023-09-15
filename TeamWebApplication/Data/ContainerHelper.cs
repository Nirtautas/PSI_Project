namespace TeamWebApplication.Data
{
    public static class ContainerHelper
    {
        public static void fetchLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            relationContainer.fetchRelationData();
            courseContainer.fetchCourses();
<<<<<<< HEAD
            userContainer.FetchUsers();
=======
            userContainer.fetchUsers();
>>>>>>> ee3f1aced4d147c0facef12d85f00e914c66aeed
            relationContainer.applyRelationData(courseContainer._courseList, userContainer._userList);
        }

        public static void writeLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            courseContainer.writeCourses();
<<<<<<< HEAD
            userContainer.WriteUsers();
=======
            userContainer.writeUsers();
>>>>>>> ee3f1aced4d147c0facef12d85f00e914c66aeed
            relationContainer.writeRelationData();
        }

        public static void printLocalData(RelationContainer relationContainer, CourseContainer courseContainer, UserContainer userContainer)
        {
            courseContainer.printCourseList();
            System.Diagnostics.Debug.WriteLine("");
<<<<<<< HEAD
            userContainer.PrintUserList();
=======
            userContainer.printUserList();
>>>>>>> ee3f1aced4d147c0facef12d85f00e914c66aeed
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
