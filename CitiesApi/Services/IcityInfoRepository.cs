using CitiesApi.Entities;

namespace CitiesApi.Services
{
    public interface IcityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City?> GetCityAsync(int cityId , bool includePointsOfInterest);

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExsitAsync(int cityId);

        Task<bool> PointOfInterestExsitAsync(int cityId, int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

      //  Task UpdatePointOfInterestAsync(int cityId, int pointOfInterestId , PointOfInterest updatedPointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterestToDelete);
        Task<int> SaveChangesAsync();
        
    }
}
