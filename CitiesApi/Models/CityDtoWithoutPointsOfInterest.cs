﻿namespace CitiesApi.Models
{
    /// <summary>
    /// A city without points of interest
    /// </summary>
    public class CityDtoWithoutPointsOfInterest
    {
        /// <summary>
        /// The id of the city
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// The name of the city
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the city 
        /// </summary>
        public string? Description { get; set; }
    }
}
