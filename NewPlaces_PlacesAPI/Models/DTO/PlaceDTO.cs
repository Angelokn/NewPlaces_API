using System.ComponentModel.DataAnnotations;

namespace NewPlaces_PlacesAPI.Models.Dto
{
    public class PlaceDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
    }
}
