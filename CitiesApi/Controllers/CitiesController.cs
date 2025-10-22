using CitiesApi.Data;
using CitiesApi.Models;
using CitiesApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
namespace CitiesApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly IcityInfoRepository _cityInfoRepository;    
        private readonly IMapper _mapper;
        const int maxCityPageSize = 20;
        
        public CitiesController(IcityInfoRepository cityInfoRepository ,
            IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDtoWithoutPointsOfInterest>>> GetCities([FromQuery] string ? name , 
            [FromQuery] string? searchQuery , int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCityPageSize) pageSize = maxCityPageSize;
            var (CityEntites , paginationMetaDate) = await _cityInfoRepository.GetCitiesAsync(name , searchQuery , pageNumber , pageSize);
            Response.Headers.Add("X-Pagination" , 
                JsonSerializer.Serialize(paginationMetaDate));
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
