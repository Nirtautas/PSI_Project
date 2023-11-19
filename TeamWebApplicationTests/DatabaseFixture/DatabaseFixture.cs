using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TeamWebApplication.Data.Database;
using TeamWebApplication.Models.Enums;
using TeamWebApplication.Models;

namespace TeamWebApplicationTests.DatabaseFixture
{
    public class DBFixture : IDisposable
    {
        public ApplicationDBContext Context { get; private set; }

        public DBFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(new Random().Next().ToString())
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            Context = new ApplicationDBContext(options);
        }

        public void PopulateData()
        {
            var user1 = new User
            {
                UserId = 20000,
                Name = "Jonas",
                Surname = "Paguzinskas",
                Email = "j.paguzinskas@mif.stuf.vu.lt",
                Password = "qweryty1",
                Role = Role.Student,
                Faculty = Faculty.MathematicsAndInformatics,
                Specialization = Specialization.ProgramSystems
            };
            var user2 = new User
            {
                UserId = 20001,
                Name = "Armandas",
                Surname = "Jarasunas",
                Email = "a.jarasunas@mif.stuf.vu.lt",
                Password = "aludaris17",
                Role = Role.Student,
                Faculty = Faculty.Physics,
                Specialization = Specialization.QuantumPhysics
            };
            var user3 = new User
            {
                UserId = 20002,
                Name = "Alita",
                Surname = "Stuknaite",
                Email = "a.stuknyte@mif.stuf.vu.lt",
                Password = "metupataikau695",
                Role = Role.Lecturer,
                Faculty = Faculty.MathematicsAndInformatics,
                Specialization = Specialization.ProgramSystems
            };
            Context.Users.AddRange(user1, user2, user3);

            var course1 = new Course
            {
                CourseId = 10000,
                Name = "Computer Architecture",
                CreationDate = DateTime.Now,
                Description = "Course for computer architecture",
                IsVisible = true,
                IsPublic = false
            };
            var course2 = new Course
            {
                CourseId = 10001,
                Name = "Functional Programming",
                CreationDate = DateTime.Now,
                Description = "Course for functional programming",
                IsVisible = false,
                IsPublic = false
            };
            var course3 = new Course
            {
                CourseId = 10002,
                Name = "Database Systems",
                CreationDate = DateTime.Now,
                Description = "Course for database systems",
                IsVisible = true,
                IsPublic = true
            };
            Context.Courses.AddRange(course1, course2, course3);

            Context.CoursesUsers.AddRange(
                new CourseUser
                {
                    CourseId = course1.CourseId,
                    UserId = user1.UserId
                },
                new CourseUser
                {
                    CourseId = course2.CourseId,
                    UserId = user1.UserId
                },
                new CourseUser
                {
                    CourseId = course1.CourseId,
                    UserId = user2.UserId
                },
                new CourseUser
                {
                    CourseId = course3.CourseId,
                    UserId = user2.UserId
                },
                new CourseUser
                {
                    CourseId = course2.CourseId,
                    UserId = user3.UserId
                }
            );

            Context.Posts.AddRange(
                new TextPost
                {
                    PostId = 30000,
                    CourseId = course1.CourseId,
                    Name = "Introduction",
                    CreationDate = DateTime.Now,
                    IsVisible = true,
                    TextContent = "This is a placeholder"
                },
                new TextPost
                {
                    PostId = 30001,
                    CourseId = course3.CourseId,
                    Name = "Knowledge",
                    CreationDate = DateTime.Now,
                    IsVisible = true,
                    TextContent = "This is once more a placeholder https://www.youtube.com/watch?v=dQw4w9WgXcQ"
                },
                new FilePost
                {
                    PostId = 30003,
                    CourseId = course2.CourseId,
                    Name = "File",
                    CreationDate = DateTime.Now,
                    IsVisible = true,
                    FileName = "tvarkarastis.jpg"
                }
            );

            Context.Comments.AddRange(
                new Comment
                {
                    CommentId = 40000,
                    CourseId = course1.CourseId,
                    UserId = user1.UserId,
                    CommentatorName = user1.Name,
                    CommentatorSurname = user1.Surname,
                    CreationTime = DateTime.Now,
                    UserComment = "Sus course"
                },
                new Comment
                {
                    CommentId = 40001,
                    CourseId = course2.CourseId,
                    UserId = user3.UserId,
                    CommentatorName = user3.Name,
                    CommentatorSurname = user3.Surname,
                    CreationTime = DateTime.Now,
                    UserComment = "good"
                },
                new Comment
                {
                    CommentId = 40002,
                    CourseId = course1.CourseId,
                    UserId = user3.UserId,
                    CommentatorName = user3.Name,
                    CommentatorSurname = user3.Surname,
                    CreationTime = DateTime.Now,
                    UserComment = "Cool"
                }
            );
            Context.SaveChanges();
        }

        public void ClearData()
        {
            Context.Users.RemoveRange(Context.Users);
            Context.Courses.RemoveRange(Context.Courses);
            Context.Posts.RemoveRange(Context.Posts);
            Context.Comments.RemoveRange(Context.Comments);
            Context.CoursesUsers.RemoveRange(Context.CoursesUsers);
            Context.SaveChanges();
        }

        public void RepopulateData()
        {
            ClearData();
            PopulateData();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
