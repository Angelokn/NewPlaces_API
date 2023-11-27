using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewPlaces_PlacesAPI.Models;

namespace NewPlaces_PlacesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Place> GetPlaces()
        {
            return new List<Place>
            {
                new Place { Id = 1, Name = "Sun Villa" },
                new Place { Id = 2, Name = "Beach View" }
            };
        }
    }
}
