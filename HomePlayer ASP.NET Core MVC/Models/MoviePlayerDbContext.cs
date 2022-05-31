using Microsoft.EntityFrameworkCore;

namespace HomePlayer_ASP.NET_Core_MVC.Models
{
    public class MoviePlayerDbContext : DbContext
    {
        public MoviePlayerDbContext(DbContextOptions<MoviePlayerDbContext> options) : base(options)
        { }

        public DbSet<Movie> Images { get; set; }
    }
}