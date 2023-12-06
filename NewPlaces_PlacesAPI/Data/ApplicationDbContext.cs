using Microsoft.EntityFrameworkCore;
using NewPlaces_PlacesAPI.Models;

namespace NewPlaces_PlacesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Place> Places { get; set; }
    }
}