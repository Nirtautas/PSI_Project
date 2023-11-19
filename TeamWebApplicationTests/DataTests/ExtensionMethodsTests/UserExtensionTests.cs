using TeamWebApplication.Data.ExtensionMethods;
using TeamWebApplication.Models;
using TeamWebApplication.Models.Enums;

namespace TeamWebApplicationTests.DataTests.ExtensionMethodsTests
{
    public class UserExtensionTests
    {
        [Fact]
        public void FormattedToString_GivingUserClass_ReturnsCorrectFormat()
        {
            var expected = "10000;John;Carter;John.Carter@gmail.com;Password1;Student;Physics;QuantumPhysics";
            var user = new User(10000, "John", "Carter", "John.Carter@gmail.com",
                "Password1", Role.Student, Faculty.Physics, Specialization.QuantumPhysics);

            var actual = user.FormattedToString();

            Assert.Equal(expected, actual);
        }
    }
}
