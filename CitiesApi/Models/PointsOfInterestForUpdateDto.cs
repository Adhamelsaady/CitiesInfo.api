using System.ComponentModel.DataAnnotations;

namespace CitiesApi.Models
{
    public class PointsOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "The name is required")]

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
