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

        public AstDevContext(DbContextOptions<AstDevContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                
            
        }
    }
}
