using AIMarketplace.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AIMarketplace.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ad> Ads { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
