using Microsoft.EntityFrameworkCore;

namespace WeatherAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Weather.Data.Weather> Weather { get; set; }
    }
}