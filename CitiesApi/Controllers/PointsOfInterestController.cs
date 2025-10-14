using CitiesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitiesApi.Controllers
{
    [Route("api/cities/{cityid}/pointsofinterest")]
    [ApiController]
    
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]   
        public ActionResult<IEnumerable<PointsOfInterestDto>> GetPointsOfInterest(int CityId)
        { 
            var City = CitiesDataStore.Current.Cities.Find(city => city.Id == CityId);
            if (City == null) 
            {
                return NotFound(); 
            }

            return Ok(City.PointsOfInterest);
        }

        [HttpGet("{PointOfInterestId}")]
        public ActionResult <PointsOfInterestDto> GetPointOfInterest(int CityId , int PointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(C => C.Id == CityId);
            if(city == null)
            {
                return NotFound();
            }
           
            var PointOfInterest = city.PointsOfInterest.FirstOrDefault(point => point.Id == PointOfInterestId);            
            if (PointOfInterest == null && city.NumberOfPointsOfInterest != 0)
                return NotFound();

            return Ok(PointOfInterest);
        }

    }
}
