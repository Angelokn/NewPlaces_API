using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id:int}", Name = "GetPlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
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
            if (PlaceStore.placeList.FirstOrDefault(u => u.Name.ToLower() == placeDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Place name already exists.");
                return BadRequest(ModelState);
            }

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

            return CreatedAtRoute("GetPlace", new { id = placeDTO.Id }, placeDTO);
        }

        [HttpPut("{id:int", Name = "UpdatePlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePlace(int id, [FromBody]PlaceDTO placeDTO)
        {
            if (id != placeDTO.Id || placeDTO == null)
            {
                return BadRequest();
            }

            var place = PlaceStore.placeList.FirstOrDefault(u => u.Id == id);
            
            place.Name = placeDTO.Name;
            place.Occupancy = placeDTO.Occupancy;
            place.Sqft = placeDTO.Sqft;

            return NoContent();
            // -m "implemented Put method and added two more properties to PlaceDTO model"
        }

        [HttpPatch("{id:int", Name = "UpdatePartialPlace")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PatchPlace(int id, JsonPatchDocument<PlaceDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var place = PlaceStore.placeList.FirstOrDefault(u => u.Id == id);

            if (place == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(place, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeletePlace")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePlace (int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var place = PlaceStore.placeList.FirstOrDefault(u => u.Id ==  id);

            if (place == null)
            {
                return NotFound();
            }

            PlaceStore.placeList.Remove(place);
            return NoContent();
        }
    }
}