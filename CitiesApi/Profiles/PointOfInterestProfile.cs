using AutoMapper;

namespace CitiesApi.Profiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile() 
        {
            CreateMap<Entities.PointOfInterest, Models.PointsOfInterestDto>();
        }

    }
}
