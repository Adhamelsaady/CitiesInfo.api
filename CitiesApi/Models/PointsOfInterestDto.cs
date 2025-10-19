namespace CitiesApi.Models
{
    public class PointsOfInterestDto
    {
        public int PointOfInterestId { get; set; } 
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

    }
}
