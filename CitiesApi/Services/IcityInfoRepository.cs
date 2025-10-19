using CitiesApi.Entities;

namespace CitiesApi.Services
{
    public interface IcityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City?> GetCityAsync(int cityId , bool includePointsOfInterest);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExsit(int cityId);
    }
}
