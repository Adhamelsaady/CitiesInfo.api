using CitiesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CitiesApi.Controllers
{
    [Route("api/cities/{cityid}/pointsofinterest")]
    [ApiController]

    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointsOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            var City = CitiesDataStore.Current.Cities.Find(city => city.Id == cityId);
            if (City == null)
            {
                return NotFound();
            }

            return Ok(City.PointsOfInterest);
        }

        [HttpGet("{PointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointsOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(C => C.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var PointOfInterest = city.PointsOfInterest.FirstOrDefault(point => point.Id == pointOfInterestId);
            if (PointOfInterest == null && city.NumberOfPointsOfInterest != 0)
                return NotFound();

            return Ok(PointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointsOfInterestDto> CreatePointOfInterest(int cityId, PointsOfInterestForCreationDto pointsOfInterest)
        {
            //  if (!ModelState.IsValid) return BadRequest();
            var City = CitiesDataStore.Current.Cities.Find(city => city.Id == cityId);
            if (City == null)
                return NotFound();
            var MaxPointOfIntersetId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var FinalPointOfInterst = new PointsOfInterestDto
            {
                Id = MaxPointOfIntersetId + 1,
                Name = pointsOfInterest.Name,
                Description = pointsOfInterest.Description
            };
            City.PointsOfInterest.Add(FinalPointOfInterst);
            return CreatedAtRoute("GetPointOfInterest",
                new {
                    cityid = cityId, PointOfInterestId = FinalPointOfInterst.Id
                }, FinalPointOfInterst);
        }


        [HttpPut("{PointOfInterestId}")]
        public ActionResult<PointsOfInterestDto> UpdatePointOfInterest(int cityId, int pointOfInterestId,
            PointsOfInterestForUpdateDto PointOfInterest)
        {
            var City = CitiesDataStore.Current.Cities.Find(C => C.Id == cityId);
            if (City == null) return NotFound();

            var CurrentPointOfInterest = City.PointsOfInterest.First(P => P.Id == pointOfInterestId);
            if (CurrentPointOfInterest == null) return NotFound();

            CurrentPointOfInterest.Name = PointOfInterest.Name;
            CurrentPointOfInterest.Description = PointOfInterest.Description;

            return NoContent();
        }


        [HttpPatch("{pointOfInterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest (int cityId, int pointOfInterestId, 
            JsonPatchDocument <PointsOfInterestForUpdateDto> patchDocument)
        {
            var City = CitiesDataStore.Current.Cities.Find(C => C.Id == cityId);
            if (City == null) 
                return NotFound();

            var CurrentPointOfInterest = City.PointsOfInterest.First(P => P.Id == pointOfInterestId);
            if (CurrentPointOfInterest == null) 
                return NotFound();

            var PointOfInterestToPatch = new PointsOfInterestForUpdateDto
            {
                Name = CurrentPointOfInterest.Name,
                Description = CurrentPointOfInterest.Description
            };
            
            patchDocument.ApplyTo(PointOfInterestToPatch , ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(PointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            CurrentPointOfInterest.Name = PointOfInterestToPatch.Name;
            CurrentPointOfInterest.Description = PointOfInterestToPatch.Description;


            return NoContent();
        }

        [HttpDelete ("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest (int cityId , int pointOfInterestId)
        {
            var City = CitiesDataStore.Current.Cities.Find(C => C.Id == cityId);
            if (City == null) return NotFound();

            var CurrentPointOfInterest = City.PointsOfInterest.First(P => P.Id == pointOfInterestId);
            if (CurrentPointOfInterest == null) return NotFound();

            City.PointsOfInterest.Remove(CurrentPointOfInterest);
            return NoContent();
        }

    }
}
