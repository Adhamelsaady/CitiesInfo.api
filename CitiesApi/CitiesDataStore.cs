using CitiesApi.Models;

namespace CitiesApi
{
    public class CitiesDataStore
    {
        public List <CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Cairo",
                    Description = "The one with the Nile River and the pyramids nearby.",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 1,
                            Name = "Giza Pyramids",
                            Description = "The iconic ancient pyramids and the Sphinx."
                        },
                        new PointsOfInterestDto()
                        {
                            Id = 2,
                            Name = "Egyptian Museum",
                            Description = "Famous for its collection of ancient Egyptian artifacts."
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Alexandria",
                    Description = "The one by the Mediterranean Sea with the famous library.",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 3,
                            Name = "Bibliotheca Alexandrina",
                            Description = "A modern library built to honor the ancient one."
                        },
                        new PointsOfInterestDto()
                        {
                            Id = 4,
                            Name = "Qaitbay Citadel",
                            Description = "A historic fortress on the Mediterranean coast."
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Luxor",
                    Description = "The one with ancient temples and the Valley of the Kings.",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 5,
                            Name = "Karnak Temple",
                            Description = "A vast complex of temples and statues."
                        },
                        new PointsOfInterestDto()
                        {
                            Id = 6,
                            Name = "Valley of the Kings",
                            Description = "Royal tombs of ancient pharaohs."
                        }
                    }
                },
                new CityDto()
                {
                    Id = 4,
                    Name = "Aswan",
                    Description = "The one with the High Dam and beautiful Nile views.",
                    PointsOfInterest = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto()
                        {
                            Id = 7,
                            Name = "Aswan High Dam",
                            Description = "A massive dam controlling the Nile River."
                        },
                        new PointsOfInterestDto()
                        {
                            Id = 8,
                            Name = "Philae Temple",
                            Description = "An ancient temple dedicated to the goddess Isis."
                        }
                    }
                }
            };
        }
    }
}
