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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult <IEnumerable<PlaceDTO>> GetPlaces()
        {
            return Ok(PlaceStore.placeList);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PlaceDTO> GetPlaceById(int id)
        {
            if (id ==0)
            {
                return BadRequest();
            }

            var place = Ok(PlaceStore.placeList.FirstOrDefault(u=>u.Id==id));

            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }
    }
}