using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Models;

namespace TeamWebApplicationTests.DataTests.ExtensionMethodsTests
{
    public class CommentExtensionTests
    {
        [Fact]
        public void FormattedToString_IsOfCorrectFormat()
        {
            var comment = new Comment(10000, 20000, 30000, "John", "Carter", "CommentData");
            var date = DateTime.Parse("2023-02-02 23:12:59");
            comment.CreationTime = date;
            var expected = "10000;20000;30000;John;Carter;2023-02-02 23:12:59;CommentData";

            var actual = comment.FormattedToString();

            Assert.Equal(expected, actual);
        }
    }
}
