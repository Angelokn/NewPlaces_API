using NewPlaces_PlacesAPI.Models.Dto;

namespace NewPlaces_PlacesAPI.Data
{
    public static class PlaceStore
    {
        public static List<PlaceDTO> placeList = new List<PlaceDTO>
            {
                new PlaceDTO { Id = 1, Name = "Sun Villa" },
                new PlaceDTO { Id = 2, Name = "Beach View" }
            };
    }
}
