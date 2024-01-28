using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> entity)
    {
        entity.HasKey(e => e.IataCode).HasName("dict_airports_pkey");

        entity.ToTable("dict_airports", "PSP", tb => tb.HasComment("Аэропорты"));

        entity.Property(e => e.IataCode)
            .HasComment("Код IATA аэропорта")
            .HasColumnType("character varying")
            .HasColumnName("iata_code");
        entity.Property(e => e.CityIataCode)
            .HasComment("Ссылка на населенный пункт расположения аэропорта")
            .HasColumnType("character varying")
            .HasColumnName("city_iata_code");
        entity.Property(e => e.IcaoCode)
            .HasComment("Код ICAO аэропорта")
            .HasColumnType("character varying")
            .HasColumnName("icao_code");
        entity.Property(e => e.Name)
            .HasComment("Название аэропорта")
            .HasColumnType("character varying")
            .HasColumnName("name");
        entity.Property(e => e.RfCode)
            .HasComment("Код аэропорта по Воздушному кодексу РФ")
            .HasColumnType("character varying")
            .HasColumnName("rf_code");

        entity.HasOne(d => d.CityIataCodeNavigation).WithMany(p => p.Airports)
            .HasForeignKey(d => d.CityIataCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("dict_airports_fkey_city_iata_code");
    }
}