using CitiesApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitiesApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet <City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterests { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City("New York City")
                    {
                        CityId = 1,
                        Description = "The one with that big park."
                    },
                    new City("Antwerp")
                    {
                        CityId = 2,
                        Description = "The one with the cathedral that was never really finished."
                    },
                    new City("Paris")
                    {
                        CityId = 3,
                        Description = "The one with that big tower."
                    }
                );
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
        // New York City
            new PointOfInterest("Central Park")
            {
                PointOfInterestId = 1,
                CityId = 1,
                Description = "A large city park in the heart of Manhattan."
            },
            new PointOfInterest("Empire State Building")
            {
                PointOfInterestId = 2,
                CityId = 1,
                Description = "A 102-story skyscraper with amazing views."
            },

            // Antwerp
            new PointOfInterest("Cathedral of Our Lady")
            {
                PointOfInterestId = 3,
                CityId = 2,
                Description = "A stunning Gothic cathedral."
            },
            new PointOfInterest("Antwerp Zoo")
            {
                PointOfInterestId = 4,
                CityId = 2,
                Description = "One of the oldest zoos in the world."
            },

            // Paris
            new PointOfInterest("Eiffel Tower")
            {
                PointOfInterestId = 5,
                CityId = 3,
                Description = "The iconic iron tower."
            },
            new PointOfInterest("Louvre Museum")
            {
                PointOfInterestId = 6,
                CityId = 3,
                Description = "The world’s largest art museum."
            }
        );
            base.OnModelCreating(modelBuilder);

        }

        
    }
}
