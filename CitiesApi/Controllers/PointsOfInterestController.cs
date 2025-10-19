using AutoMapper;
using CitiesApi.Data;
using CitiesApi.Models;
using CitiesApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CitiesApi.Controllers
{
    [Route("api/cities/{cityid}/pointsofinterest")]
    [ApiController]

    public class PointsOfInterestController : ControllerBase
    {

        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailservice;
        private readonly IcityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public PointsOfInterestController(
            ILogger <PointsOfInterestController> logger
            , IMailService mailService
            ,IcityInfoRepository cityInfoRepository
            , IMapper mapper)
        {
            _logger = logger
                        ?? throw new ArgumentNullException(nameof(_logger));
            _mailservice = mailService 
                        ?? throw new ArgumentNullException(nameof(_mailservice));
            _cityInfoRepository = cityInfoRepository 
                        ?? throw new ArgumentNullException(nameof(_cityInfoRepository));
            _mapper = mapper 
                        ?? throw new ArgumentNullException(nameof(_mapper));
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointsOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if (!await _cityInfoRepository.CityExsit(cityId))
            {
                _logger.LogInformation($"City with id = {cityId} does not exsit");
                return NotFound();
            }
            var PointsOfInterest = await _cityInfoRepository.GetPointsOfInterestAsync(cityId);

            if (PointsOfInterest == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<PointsOfInterestDto>>(PointsOfInterest));            
        }

        /*

        [HttpGet("{PointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointsOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = _dataStore.Cities.FirstOrDefault(C => C.Id == cityId);
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
            var City = _dataStore.Cities.Find(city => city.Id == cityId);
            if (City == null)
                return NotFound();
            var MaxPointOfIntersetId = _dataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

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
            var City = _dataStore.Cities.Find(C => C.Id == cityId);
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
            var City = _dataStore.Cities.Find(C => C.Id == cityId);
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
            var City = _dataStore.Cities.Find(C => C.Id == cityId);
            if (City == null) return NotFound();

            var CurrentPointOfInterest = City.PointsOfInterest.First(P => P.Id == pointOfInterestId);
            if (CurrentPointOfInterest == null) return NotFound();


            City.PointsOfInterest.Remove(CurrentPointOfInterest);
            _mailservice.Send("Deleted point of interest", $"point {CurrentPointOfInterest.Name} has been deleted");
            return NoContent();
        }
        */

    }
}
