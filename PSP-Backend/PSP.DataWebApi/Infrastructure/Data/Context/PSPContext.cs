using Domain.Models;
using Infrastructure.Data.Context.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context;

public class PSPContext : DbContext
{
    public PSPContext(DbContextOptions<PSPContext> options) : base(options)
    {
    }
    public virtual DbSet<CouponEvent> CouponEvents { get; set; }
    public virtual DbSet<Passenger> Passengers { get; set; }
    public virtual DbSet<Airline> Airlines { get; set; }
    public virtual DbSet<Airport> Airports { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<DocumentType> DocumentTypes { get; set; }
    public virtual DbSet<Flight> Flights { get; set; }
    public virtual DbSet<GenderType> Genders { get; set; }
    public virtual DbSet<PassengerType> PassengerTypes { get; set; }
    public virtual DbSet<QuotaCategory> QuotaCategories { get; set; }
    public virtual DbSet<SubsidizedRoute> SubsidizedRoutes { get; set; }
    public virtual DbSet<TicketType> TicketTypes { get; set; }
    public virtual DbSet<OperationType> OperationTypes { get; set; }
    public virtual DbSet<Fare> Fare { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_ROUTE") ?? "Server=localhost; Port=5432;Database=PSP_Data;User Id=postgres;Password=1243");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
