using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Models;

namespace TeamWebApplicationTests.DataTests.ExtensionMethodsTests
{
    public class CourseExtensionTests
    {
        [Fact]
        public void FormattedToString_IsOfCorrectFormat()
        {
            var course = new Course(10000, "Course1", "Course Description", true, true);
            var date = DateTime.Parse("2023-02-02 23:12:59");
            course.CreationDate = date;
            var expected = "10000;Course1;2023-02-02 23:12:59;Course Description;True;True";

            var actual = course.FormattedToString();

            Assert.Equal(expected, actual);
        }
    }
}
