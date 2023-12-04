using Microsoft.EntityFrameworkCore;
using NewPlaces_PlacesAPI.Models;

namespace NewPlaces_PlacesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Place> Places { get; set; }
    }
}