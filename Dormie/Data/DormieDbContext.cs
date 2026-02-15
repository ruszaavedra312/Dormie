using Dormie.Models;
using Microsoft.EntityFrameworkCore;

namespace Dormie.Data
{
    public class DormieDbContext : DbContext
    {
        public DormieDbContext(DbContextOptions<DormieDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.UserType)
                .HasConversion<string>();  // Store enum as string
        }
    }
}
