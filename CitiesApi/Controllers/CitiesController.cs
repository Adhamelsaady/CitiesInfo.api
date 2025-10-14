using CitiesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
namespace CitiesApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            var result = CitiesDataStore.Current.Cities;
            return Ok(result);
        }
        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var CityToReturn = CitiesDataStore.Current.Cities.Find(c => c.Id == id);
            return CityToReturn == null ? NotFound() : Ok(CityToReturn);
        }
    }
}
