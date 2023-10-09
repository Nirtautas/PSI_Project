using Microsoft.EntityFrameworkCore;
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

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
