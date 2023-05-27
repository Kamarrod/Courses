using Courses.Domain.Entity;
using Courses.Domain.Enum;
using Courses.Domain.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Сourses.Domain.Entity;

namespace Courses.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        // Создание базы данных, если ее не существует
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Course> Course { get; set; }
        public DbSet<PracticalPart> PracticalParts { get; set; }
        public DbSet<CompletedPart> CompletedParts { get; set; }
        public DbSet<CompletedCourse> CompletedCourse { get; set; }
        public DbSet<SubscribedCourse> SubscribedCourse { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompletedCourse>(entity =>
                {
                    entity.ToTable("CompletedCourse");
                    entity.HasKey(c => new { c.UserId, c.CourseId });
                    entity.Property(e => e.UserId).IsRequired();
                    entity.Property(e => e.CourseId).IsRequired();
                });

            modelBuilder.Entity<CompletedPart>(entity =>
            {
                entity.ToTable("CompletedPart");
                entity.HasKey(c => new { c.UserId, c.PracticalPartId });
                entity.Property(e => e.PracticalPartId).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
            });

            modelBuilder.Entity<SubscribedCourse>(entity =>
            {
                entity.ToTable("SubscribedCourse");
                entity.HasKey(c => new { c.UserId, c.CourseId });
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.CourseId).IsRequired();
            });
        }
    }
}
