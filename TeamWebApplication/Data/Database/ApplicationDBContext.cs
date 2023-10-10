using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TeamWebApplication.Migrations;
using TeamWebApplication.Models;

namespace TeamWebApplication.Data.Database
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TextPost> TextPosts { get; set; }
        public DbSet<LinkPost> LinkPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
		public DbSet<CourseUser> CoursesUsers { get; set; }

		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
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
			modelBuilder.Entity<User>().HasData(user1, user2, user3);

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

			base.OnModelCreating(modelBuilder);
        }
    }
}
