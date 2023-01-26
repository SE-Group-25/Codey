using Microsoft.EntityFrameworkCore;
using JetBrains.Annotations;
using SE_AI_Skills_Tool.Models;

namespace SE_AI_Skills_Tool.Context
{
    public class ASTDevContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }
    }
}
