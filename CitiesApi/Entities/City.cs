using CitiesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace CitiesApi.Entities
{
    public class City
    {
       
        public int CityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; } 
            = new List<PointOfInterest>();


        public City(string name)
        {
            Name = name;
        }
    }
}
