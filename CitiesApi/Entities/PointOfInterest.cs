using System.ComponentModel.DataAnnotations;

namespace CitiesApi.Entities
{
    public class PointOfInterest
    {
        public int PointOfInterestId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public City? city { get; set; }

        public int CityId { get; set; }

        public PointOfInterest(string name)
        {
            Name = name;
        }

    }
}
