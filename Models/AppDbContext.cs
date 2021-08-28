using Microsoft.EntityFrameworkCore;

namespace WeatherAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Weather> Weather { get; set; }
        public DbSet<Token.Token> Tokens { get; set; }
    }
}