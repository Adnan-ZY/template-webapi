using Microsoft.EntityFrameworkCore;
using Template.Core.Entities;

namespace Template.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }

    }
}
