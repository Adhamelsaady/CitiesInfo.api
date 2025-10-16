using CitiesApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
namespace CitiesApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore _dataStore;

        public CitiesController(CitiesDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            var result = _dataStore.Cities;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var CityToReturn = _dataStore.Cities.Find(c => c.Id == id);
            return CityToReturn == null ? NotFound() : Ok(CityToReturn);
        }

    }
}
