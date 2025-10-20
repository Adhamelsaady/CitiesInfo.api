using AutoMapper;
using CitiesApi.Data;
using CitiesApi.Entities;
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
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if (!await _cityInfoRepository.CityExsitAsync(cityId))
            {
                _logger.LogInformation($"City with id = {cityId} does not exsit");
                return NotFound();
            }
            var PointsOfInterest = await _cityInfoRepository.GetPointsOfInterestAsync(cityId);

            if (PointsOfInterest == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(PointsOfInterest));            
        }

        

        [HttpGet("{PointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if(!await _cityInfoRepository.CityExsitAsync(cityId))
            { 
                return NotFound();
            }
            var NeededPointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if(NeededPointOfInterest == null)
                return NotFound();
            return Ok(_mapper.Map<PointOfInterestDto> (NeededPointOfInterest));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId 
            , PointOfInterestForCreationDto pointOfInterestForCreation)
        {
            if(!await _cityInfoRepository.CityExsitAsync(cityId))
            {
                return NotFound();
            }

            var FinalPointOfInterest = _mapper.Map<PointOfInterest> (pointOfInterestForCreation);    
            await _cityInfoRepository.AddPointOfInterestForCityAsync (cityId, FinalPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();
            var returnedPointOfInterest = _mapper.Map<Models.PointOfInterestDto>(FinalPointOfInterest);


            return CreatedAtRoute("GetPointOfInterest" ,
                new 
                {   CityId = cityId ,
                    FinalPointOfInterest.PointOfInterestId
                }
                ,returnedPointOfInterest);
        }

        [HttpPut("{PointOfInterestId}")]
        
        public async Task <ActionResult> UpdatePointOfInterest(int cityId , int pointOfInterestId
            , PointOfInterestForUpdateDto pointOfInterestForUpdate
            )
        {
            if (!await _cityInfoRepository.CityExsitAsync(cityId))
                return NotFound();

            var CurrentPointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            
            if(CurrentPointOfInterest == null) 
                return NotFound();

            _mapper.Map(pointOfInterestForUpdate, CurrentPointOfInterest);

            await _cityInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete ("{pointOfInterestId}")]

        public async Task <ActionResult> DeletePointOfInterest( int cityId , int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExsitAsync(cityId))
                return NotFound();
            var CurrentPointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (CurrentPointOfInterest == null)
                return NotFound();

            _cityInfoRepository.DeletePointOfInterest(CurrentPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();
            _mailservice.Send("Deleted point of interest", $"point {CurrentPointOfInterest.Name} has been deleted");
            return NoContent();

        }

        
        [HttpPatch("{pointOfInterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest (int cityId, int pointOfInterestId, 
            JsonPatchDocument <PointOfInterestForUpdateDto> patchDocument)
        {
            if (!await _cityInfoRepository.CityExsitAsync(cityId))
                return NotFound();

            var CurrentPointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (CurrentPointOfInterest == null)
                return NotFound();


            var PointOfInterestToPatch =  _mapper.Map<Models.PointOfInterestForUpdateDto>
                (CurrentPointOfInterest);

            
            
            patchDocument.ApplyTo(PointOfInterestToPatch , ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(PointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(PointOfInterestToPatch , CurrentPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        
        

    }
}
