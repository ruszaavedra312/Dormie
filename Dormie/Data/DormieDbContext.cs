using Dormie.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dormie.Data
{
    public class DormieDbContext : DbContext
    {
        public DormieDbContext(DbContextOptions<DormieDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<User> Users { get; set; }
    }
}
