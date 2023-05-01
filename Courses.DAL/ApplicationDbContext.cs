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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Course> Course { get; set; }

        public DbSet<PracticalPart> PracticalParts { get; set; }

        ////public DbSet<User> Users { get; set; }

        public DbSet<CompletedPart> CompletedParts { get; set; }

        public DbSet<CompletedCourse> CompletedCourse { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PracticalPart>()
        //        .HasKey(l => new { l.CourseId, l.Number });
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ////modelBuilder.Entity<PracticalPart>()
            ////    .HasKey(c => new { c.CourseId, c.Id });
            //modelBuilder.Entity<User>(builder =>
            //{
            //    //builder.ToTable("Users").HasKey(x => x.Id);
            //    //builder.HasKey(c => c.Id);

            //    builder.HasData(new User[]
            //    {
            //        new User()
            //        {
            //            Id = 1,
            //            Name = "Admin",
            //            Password = HashPasswordHelper.HashPassword("123456"),
            //            Role = Role.Admin
            //        }
            //    });
            //});
            ////modelBuilder.Entity<User>(entity =>
            ////{
            ////    entity.ToTable("Users");
            ////    entity.HasKey(e => e.Id);
            ////    entity.Property(e => e.Name).IsRequired();
            ////    entity.Property(e => e.Password).IsRequired();
            ////    //entity.Property(e => e.Role).IsRequired();

            ////    entity.HasData(new User[]
            ////    {
            ////            new User()
            ////            {
            ////                Id = 1,
            ////                Name = "Admin",
            ////                Password = HashPasswordHelper.HashPassword("123456"),
            ////                //Role = Role.Admin
            ////            }
            ////    });
            ////});



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
                entity.HasKey(c => new { c.UserId, c.PraticalPartId });
                entity.Property(e => e.PraticalPartId).IsRequired();
                entity.Property(e => e.UserId).IsRequired();
            });
        }

    }

}
