using CitiesApi.Data;
using CitiesApi.Models;
using CitiesApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;
using AutoMapper;
namespace CitiesApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly IcityInfoRepository _cityInfoRepository;    
        private readonly IMapper _mapper;    
        
        public CitiesController(IcityInfoRepository cityInfoRepository ,
            IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDtoWithoutPointsOfInterest>>> GetCities()
        {
            var CityEntites = await _cityInfoRepository.GetCitiesAsync();
            return Ok(_mapper.Map<IEnumerable<CityDtoWithoutPointsOfInterest>>(CityEntites));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id , bool includePointsOfInterest)
        {
            var CityToReturn = await _cityInfoRepository.GetCityAsync(id , includePointsOfInterest);
            if(CityToReturn == null) 
                return NotFound();

            if(includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto> (CityToReturn));
            }
            return Ok(_mapper.Map<CityDtoWithoutPointsOfInterest>(CityToReturn));
        }

    }
}
