using System.ComponentModel.DataAnnotations;

namespace NewPlaces_PlacesAPI.Models.Dto
{
    public class PlaceDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public string Details { get; set; }
        public double Rate { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int Sqft { get; set; }
        public string ImageUrl { get; set; }

    }
}
