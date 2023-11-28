using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewPlaces_PlacesAPI.Data;
using NewPlaces_PlacesAPI.Models;
using NewPlaces_PlacesAPI.Models.Dto;

namespace NewPlaces_PlacesAPI.Controllers
{
    [Route("api/PlacesAPI")]
    [ApiController]
    public class PlacesAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<PlaceDTO> GetPlaces()
        {
            return PlaceStore.placeList;
        }
    }
}