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
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PlacesAPIController> _logger;

        public PlacesAPIController(ApplicationDbContext db, ILogger<PlacesAPIController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PlaceDTO>> GetPlaces()
        {
            _logger.LogInformation("Getting all places");
            return Ok(_db.Places.ToList());
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
                _logger.LogError("Get Place Error with Id " + id);
                return BadRequest();
            }

            var place = _db.Places.FirstOrDefault(u=>u.Id==id);

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
            if (_db.Places.FirstOrDefault(u => u.Name.ToLower() == placeDTO.Name.ToLower()) != null)
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

            Place model = new()
            {
                Id = placeDTO.Id,
                Name = placeDTO.Name,
                Details = placeDTO.Details,
                Occupancy = placeDTO.Occupancy,
                ImageUrl = placeDTO.ImageUrl,
                Rate = placeDTO.Rate,
                Sqft = placeDTO.Sqft
            };

            //considering the user will always enter a distinct Id:

            //placeDTO.Id = PlaceStore.placeList.OrderByDescending(u=>u.Id).FirstOrDefault().Id + 1;
            //PlaceStore.placeList.Add(placeDTO);

            _db.Places.Add(model);
            _db.SaveChanges();
            
            return CreatedAtRoute("GetPlace", new { id = placeDTO.Id }, placeDTO);
        }

        [HttpPut("{id:int}", Name = "UpdatePlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePlace(int id, [FromBody]PlaceDTO placeDTO)
        {
            if (id != placeDTO.Id || placeDTO == null)
            {
                return BadRequest();
            }

            //var place = PlaceStore.placeList.FirstOrDefault(u => u.Id == id);            
            //place.Name = placeDTO.Name;
            //place.Occupancy = placeDTO.Occupancy;
            //place.Sqft = placeDTO.Sqft;

            Place model = new()
            {
                Id = placeDTO.Id,
                Name = placeDTO.Name,
                Details = placeDTO.Details,
                Occupancy = placeDTO.Occupancy,
                ImageUrl = placeDTO.ImageUrl,
                Rate = placeDTO.Rate,
                Sqft = placeDTO.Sqft
            };

            _db.Places.Update(model);
            _db.SaveChanges();

            return NoContent();
            // -m "implemented Put method and added two more properties to PlaceDTO model"
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialPlace")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PatchPlace(int id, JsonPatchDocument<PlaceDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var place = _db.Places.FirstOrDefault(u => u.Id == id);

            if (place == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PlaceDTO placeDTO = new()
            {
                Id = place.Id,
                Name = place.Name,
                Details = place.Details,
                Occupancy = place.Occupancy,
                ImageUrl = place.ImageUrl,
                Rate = place.Rate,
                Sqft = place.Sqft
            };

            patchDTO.ApplyTo(placeDTO, ModelState);

            Place model = new()
            {
                Id = placeDTO.Id,
                Name = placeDTO.Name,
                Details = placeDTO.Details,
                Occupancy = placeDTO.Occupancy,
                ImageUrl = placeDTO.ImageUrl,
                Rate = placeDTO.Rate,
                Sqft = placeDTO.Sqft
            };

            _db.Places.Update(model);
            _db.SaveChanges();

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

            var place = _db.Places.FirstOrDefault(u => u.Id ==  id);

            if (place == null)
            {
                return NotFound();
            }

            _db.Places.Remove(place);
            _db.SaveChanges();

            return NoContent();
        }
    }
}