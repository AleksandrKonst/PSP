using Microsoft.EntityFrameworkCore;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context.EntityConfiguration;

namespace PSP.Infrastructure.Data.Context;

public class PSPContext : DbContext
{
    public PSPContext(DbContextOptions<PSPContext> options) : base(options)
    {
    }

    public virtual DbSet<PartOfFlight> ConFlightParts { get; set; }
    public virtual DbSet<CouponEvent> DataCouponEvents { get; set; }
    public virtual DbSet<Passenger> DataPassengers { get; set; }
    public virtual DbSet<Airline> DictAirlines { get; set; }
    public virtual DbSet<Airport> DictAirports { get; set; }
    public virtual DbSet<City> DictCities { get; set; }
    public virtual DbSet<DocumentType> DictDocumentTypes { get; set; }
    public virtual DbSet<Flight> DictFlights { get; set; }
    public virtual DbSet<GenderType> DictGenders { get; set; }
    public virtual DbSet<PassengerType> DictPassengerTypes { get; set; }
    public virtual DbSet<QuotaCategory> DictQuotaCategories { get; set; }
    public virtual DbSet<SubsidizedRoute> DictSubsidizedRoutes { get; set; }
    public virtual DbSet<TicketType> DictTicketTypes { get; set; }
    public virtual DbSet<OperationType> DictOperationTypes { get; set; }
    public virtual DbSet<Fare> DictFare { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost; Port=5432;Database=PSP_Data;User Id=postgres;Password=1243");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PartOfFlightConfiguration());
        modelBuilder.ApplyConfiguration(new CouponEventConfiguration());
        modelBuilder.ApplyConfiguration(new PassengerConfiguration());
        modelBuilder.ApplyConfiguration(new AirlineConfiguration());
        modelBuilder.ApplyConfiguration(new AirportConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FlightConfiguration());
        modelBuilder.ApplyConfiguration(new GenderTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PassengerTypeConfiguration());
        modelBuilder.ApplyConfiguration(new QuotaCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new SubsidizedRouteConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FareConfiguration());
    }
}
