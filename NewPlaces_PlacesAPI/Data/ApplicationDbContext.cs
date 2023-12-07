using Microsoft.EntityFrameworkCore;
using NewPlaces_PlacesAPI.Models;
using NewPlaces_PlacesAPI.Models.Dto;

namespace NewPlaces_PlacesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>().HasData(
                new Place()
                {
                    Id = 1,
                    Name = "Sun Villa",
                    Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg",
                    Occupancy = 6,
                    Rate = 800,
                    Sqft = 650
                },
                new Place
                {
                    Id = 2,
                    Name = "Beach View",
                    Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg",
                    Occupancy = 6,
                    Rate = 800,
                    Sqft = 550
                },
                new Place
                {
                    Id = 3,
                    Name = "Premium Confort",
                    Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                    Occupancy = 4,
                    Rate = 800,
                    Sqft = 450

                },
                new Place
              {
                  Id = 4,
                  Name = "Luxury Bungalow",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg",
                  Occupancy = 8,
                  Rate = 800,
                  Sqft = 750
              },
              new Place
              {
                  Id = 5,
                  Name = "Diamond Mountain",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg",
                  Occupancy = 8,
                  Rate = 800,
                  Sqft = 850
              }
              );
        }
    }
}