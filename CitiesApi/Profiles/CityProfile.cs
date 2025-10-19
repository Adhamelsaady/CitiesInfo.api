using AutoMapper;

namespace CitiesApi.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile() 
        {
            CreateMap<Entities.City, Models.CityDtoWithoutPointsOfInterest>();
            CreateMap<Entities.City, Models.CityDto>();
        }
    }
}
