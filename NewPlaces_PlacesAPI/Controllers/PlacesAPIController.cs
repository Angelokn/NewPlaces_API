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

            var place = PlaceStore.placeList.FirstOrDefault(u=>u.Id==id);

            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PlaceDTO> CreatePlace([FromBody]PlaceDTO placeDTO)
        {
            if (placeDTO == null)
            {
                return BadRequest(placeDTO);
            }
            if (placeDTO.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //considering the user will always enter a distinct Id (for now):
            placeDTO.Id = PlaceStore.placeList.OrderByDescending(u=>u.Id).FirstOrDefault().Id + 1;
            PlaceStore.placeList.Add(placeDTO);

            return Ok(placeDTO);
        }
    }
}