using Microsoft.EntityFrameworkCore;
using TeamWebApplicationAPI.Models;
using TeamWebApplicationAPI.Models.Enums;

namespace TeamWebApplicationAPI.Data.Database
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CourseUser> CoursesUsers { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            //this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User seeding
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
            modelBuilder.Entity<User>().HasData(user1, user2, user3);

            //Course seeding
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
            modelBuilder.Entity<Course>().HasData(course1, course2, course3);

            //CourseUser seeding
            modelBuilder.Entity<CourseUser>()
            .HasKey(t => new { t.CourseId, t.UserId });

            modelBuilder.Entity<CourseUser>()
                .HasOne(t => t.User)
                .WithMany(t => t.Courses)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<CourseUser>()
                .HasOne(t => t.Course)
                .WithMany(t => t.Users)
                .HasForeignKey(t => t.CourseId);

            modelBuilder.Entity<CourseUser>().HasData(
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

			//Post seeding
			modelBuilder.Entity<Post>()
				.HasDiscriminator(t => t.PostType)
				.HasValue<TextPost>(PostType.Text)
				.HasValue<FilePost>(PostType.File);

            modelBuilder.Entity<TextPost>().HasData(
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
                }
            );

            modelBuilder.Entity<FilePost>().HasData(
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

            //Comment seeding
            modelBuilder.Entity<Comment>();

            modelBuilder.Entity<Comment>().HasData(
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
                    CommentId = 40003,
                    CourseId = course1.CourseId,
                    UserId = user3.UserId,
                    CommentatorName = user3.Name,
                    CommentatorSurname = user3.Surname,
                    CreationTime = DateTime.Now,
                    UserComment = "Cool"
                }
            );

            //Rating seeding
            modelBuilder.Entity<Rating>()
                .HasKey(t => new { t.UserId, t.CourseId });

            modelBuilder.Entity<Rating>()
                .HasOne(t => t.User)
                .WithMany(t => t.Ratings)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Rating>()
                .HasOne(t => t.Course)
                .WithMany(t => t.Ratings)
                .HasForeignKey(t => t.CourseId);

            modelBuilder.Entity<Rating>().HasData(
                new Rating(user1.UserId, course1.CourseId, 3),
                new Rating(user1.UserId, course2.CourseId, 5),
                new Rating(user2.UserId, course1.CourseId, 4),
                new Rating(user3.UserId, course1.CourseId, 3)
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}