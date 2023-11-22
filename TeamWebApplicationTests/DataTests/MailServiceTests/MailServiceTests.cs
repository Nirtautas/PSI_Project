using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWebApplication.Controllers.ControllerEventArgs;
using TeamWebApplication.Data.ExceptionLogger;
using TeamWebApplication.Data.MailService;
using TeamWebApplication.Models;

namespace TeamWebApplicationTests.DataTests.MailServiceTests
{
    public class MailServiceTests
    {
        private const string TestDirectory = @".\Test\";

        [Fact]
        public void OnRegistration_PassingUser_ReturnsMailSubject()
        {
            var logger = new DataLogger(TestDirectory);
            MailService mailService = new MailService(logger);

            var user = new User
            {
                Name = "TestUser",
                Email = "test@example.com",
                UserId = 123
            };

            var registrationEventArgs = new RegistrationEventArgs(user);
            mailService.OnRegistration(this, registrationEventArgs);
            var expected = "Welcome to Mudli TestUser!";
            Assert.Equal(expected, mailService.MailSubject);
        }

        [Fact]
        public void OnRegistration_PassingUser_ReturnsMailBody()
        {
            var logger = new DataLogger(TestDirectory);
            MailService mailService = new MailService(logger);

            var user = new User
            {
                Name = "TestUser",
                Email = "test@example.com",
                UserId = 123
            };

            var registrationEventArgs = new RegistrationEventArgs(user);
            mailService.OnRegistration(this, registrationEventArgs);
            var expected = $"Welcome to Mudli! Your user identification code is 123. Happy learning!";
            Assert.Equal(expected, mailService.MailBody);
        }

        [Fact]
        public void OnAttendanceChange_PassingUserAndCourse_Added_ReturnsMailBodyAndSubject()
        {
            var logger = new DataLogger(TestDirectory);
            MailService mailService = new MailService(logger);

            var user = new User
            {
                Name = "TestUser",
                Email = "test@example.com",
                UserId = 123
            };
            var course = new Course
            {
                Name = "TestCourse",
                Description = "Test Decription"
            };

            var attendanceEventArgs = new AttendanceEventArgs(user, course, true);
            mailService.OnAttendanceChange(this, attendanceEventArgs);
            var expected = "You have been added to course TestCourse!";
            Assert.Equal(expected, mailService.MailSubject);
            expected = $"Dear TestUser," + Environment.NewLine + $"You have been added to course TestCourse!";
            Assert.Equal(expected, mailService.MailBody);
        }

        [Fact]
        public void OnAttendanceChange_PassingUserAndCourse_Removed_ReturnsMailBodyAndSubject()
        {
            var logger = new DataLogger(TestDirectory);
            MailService mailService = new MailService(logger);

            var user = new User
            {
                Name = "TestUser",
                Email = "test@example.com",
                UserId = 123
            };
            var course = new Course
            {
                Name = "TestCourse",
                Description = "Test Decription"
            };

            var attendanceEventArgs = new AttendanceEventArgs(user, course, false);
            mailService.OnAttendanceChange(this, attendanceEventArgs);
            var expected = "You have been removed from course TestCourse!";
            Assert.Equal(expected, mailService.MailSubject);
            expected = $"Dear TestUser," + Environment.NewLine + $"You have been removed from course TestCourse!";
            Assert.Equal(expected, mailService.MailBody);
        }
    }
}
