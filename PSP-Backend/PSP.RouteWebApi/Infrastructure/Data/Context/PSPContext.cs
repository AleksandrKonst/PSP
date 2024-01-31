using Domain.Models;
using Infrastructure.Data.Context.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class PSPContext : DbContext
{
    public PSPContext(DbContextOptions<PSPContext> options) : base(options)
    {
    }

    public virtual DbSet<FlightSegment> FlightParts { get; set; }
    public virtual DbSet<Airline> Airlines { get; set; }
    public virtual DbSet<Airport> Airports { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Flight> Flights { get; set; }
    public virtual DbSet<SubsidizedRoute> SubsidizedRoutes { get; set; }
    public virtual DbSet<Fare> Fare { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost; Port=5432;Database=PSP_Data;User Id=postgres;Password=1243");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new FlightSegmentConfiguration());
        modelBuilder.ApplyConfiguration(new AirlineConfiguration());
        modelBuilder.ApplyConfiguration(new AirportConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new FlightConfiguration());
        modelBuilder.ApplyConfiguration(new SubsidizedRouteConfiguration());
        modelBuilder.ApplyConfiguration(new FareConfiguration());
    }
}
