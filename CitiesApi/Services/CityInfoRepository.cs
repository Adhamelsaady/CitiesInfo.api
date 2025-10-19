using CitiesApi.Data;
using CitiesApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitiesApi.Services
{
    public class CityInfoRepository : IcityInfoRepository
    {
        private readonly AppDbContext _context;
        public CityInfoRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId , bool includePointsOfInterest)
        {
            if(includePointsOfInterest) 
                return await _context.Cities.Include(c => c.PointsOfInterest).Where(c => c.CityId == cityId).FirstOrDefaultAsync();
            else
                return await _context.Cities.Where(c => c.CityId == cityId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId)
        {
            return await _context.PointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
        }
        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId
            , int pointOfInterestId)
        {
            return await _context.PointOfInterests
                .FirstOrDefaultAsync(P => P.PointOfInterestId == pointOfInterestId && P.CityId == cityId);
        }

        public async Task<bool> CityExsit(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.CityId == cityId);
        }
        
    }
}
