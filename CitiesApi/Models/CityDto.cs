namespace CitiesApi.Models
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public int NumberOfPointsOfInterest 
        { 
            get 
            {
                return PointsOfInterest.Count;   
            } 
        }

        public ICollection <PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
    }
}
