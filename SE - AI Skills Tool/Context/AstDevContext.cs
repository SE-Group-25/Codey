using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using JetBrains.Annotations;
using SE_AI_Skills_Tool.Models;

namespace SE_AI_Skills_Tool.Context
{
    public class AstDevContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        public AstDevContext(DbContextOptions<AstDevContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserCourse>()
                   .HasKey(uc => new { uc.UserId, uc.CourseId });
            
            builder.Entity<UserCourse>()
                        .HasOne(uc => uc.User)
                        .WithMany(u => u.UserCourses)
                        .HasForeignKey(uc => uc.UserId);

            builder.Entity<UserCourse>()
                        .HasOne(uc => uc.Course)
                        .WithMany(c => c.UserCourses)
                        .HasForeignKey(uc => uc.CourseId);
        }
    }
}
