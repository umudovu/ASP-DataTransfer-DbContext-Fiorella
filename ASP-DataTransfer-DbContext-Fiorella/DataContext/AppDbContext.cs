using ASP_DataTransfer_DbContext_Fiorella.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_DataTransfer_DbContext_Fiorella.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Experts> Experts { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Slider> Sliders { get; set; }
    }
}
