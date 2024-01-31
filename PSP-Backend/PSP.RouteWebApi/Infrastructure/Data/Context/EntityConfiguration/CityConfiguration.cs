using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context.EntityConfiguration;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> entity)
    {
        entity.HasKey(e => e.IataCode).HasName("dict_cities_pkey");

        entity.ToTable("dict_cities", "PSP", tb => tb.HasComment("Населенные пункты"));

        entity.Property(e => e.IataCode)
            .HasComment("Код IATA населенного пункта")
            .HasColumnType("character varying")
            .HasColumnName("iata_code");
        entity.Property(e => e.Name)
            .HasComment("Название населенного пункта")
            .HasColumnType("character varying")
            .HasColumnName("name");
    }
}