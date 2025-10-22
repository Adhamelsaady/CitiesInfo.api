using CitiesApi.Data;
using CitiesApi.Entities;
using CityInfo.API.Services;
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

        public async Task<(IEnumerable<City> , PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            var colliction = _context.Cities as IQueryable<City>;

            if(!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                colliction = colliction.Where(c => c.Name == name);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                colliction = colliction.Where(
                        c => c.Name.Contains(searchQuery) || 
                        (c.Description != null && c.Description.Contains(searchQuery))
                    );
            }
            int totalItemCount = await colliction.CountAsync();
            var returnedCollection = await colliction.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (returnedCollection, new PaginationMetadata(totalItemCount , pageSize ,pageNumber));
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

        public async Task<bool> CityExsitAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.CityId == cityId);
        }

        public async Task<bool> CityNameMatchesId(string cityName, int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Name == cityName && c.CityId == cityId);
        }

        public async Task<bool> PointOfInterestExsitAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterests
                          .AnyAsync(p => p.CityId == cityId && p.PointOfInterestId == pointOfInterestId);
        }
        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var City = await GetCityAsync(cityId, false);
            if (City == null) 
                return;
            City.PointsOfInterest.Add(pointOfInterest);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterestToDelete)
        {
            _context.PointOfInterests.Remove(pointOfInterestToDelete);
        }
    }
}
