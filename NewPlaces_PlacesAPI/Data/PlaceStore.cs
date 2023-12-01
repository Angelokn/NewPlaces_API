using NewPlaces_PlacesAPI.Models.Dto;

namespace NewPlaces_PlacesAPI.Data
{
    public static class PlaceStore
    {
        public static List<PlaceDTO> placeList = new List<PlaceDTO>
            {
                new PlaceDTO { Id = 1, Name = "Sun Villa", Occupancy = 4, Sqft = 150 },
                new PlaceDTO { Id = 2, Name = "Beach View", Occupancy = 6, Sqft = 300  }
            };
    }
}
