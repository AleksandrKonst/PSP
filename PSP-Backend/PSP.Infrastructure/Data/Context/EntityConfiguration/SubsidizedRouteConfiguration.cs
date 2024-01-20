using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class SubsidizedRouteConfiguration : IEntityTypeConfiguration<SubsidizedRoute>
{
    public void Configure(EntityTypeBuilder<SubsidizedRoute> entity)
    {
        entity.HasKey(e => e.Id).HasName("dict_subsidized_routes_pkey");

        entity.ToTable("dict_subsidized_routes", "PSP", tb => tb.HasComment("Субсидированные направления"));

        entity.Property(e => e.Id)
            .HasComment("Идентификатор субсидированного направления")
            .HasColumnName("id");
        entity.Property(e => e.Appendix)
            .HasComment("Номер приложения к Постановлению Правительства РФ №215")
            .HasColumnName("appendix");
        entity.Property(e => e.CityFinishIataCode)
            .HasComment("Ссылка на конечный населенный пункт направления")
            .HasColumnType("character varying")
            .HasColumnName("city_finish_iata_code");
        entity.Property(e => e.CityStartIataCode)
            .HasComment("Ссылка на начальный населенный пункт направления")
            .HasColumnType("character varying")
            .HasColumnName("city_start_iata_code");
        entity.Property(e => e.Currency)
            .HasComment("Валюта")
            .HasColumnType("character varying")
            .HasColumnName("currency");
        entity.Property(e => e.FareAmount)
            .HasComment("Размер специального тарифа для направления")
            .HasColumnName("fare_amount");
        entity.Property(e => e.InteriorCities)
            .HasComment("Список промежуточных населенных пунктов направления")
            .HasColumnType("character varying[]")
            .HasColumnName("interior_cities");
        entity.Property(e => e.SubsidyAmount)
            .HasComment("Размер субсидии для направления")
            .HasColumnName("subsidy_amount");
        entity.Property(e => e.ValidityFrom)
            .HasComment("Начало периода актуальности записи")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("validity_from");
        entity.Property(e => e.ValidityTo)
            .HasComment("Окончание периода актуальности записи")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("validity_to");

        entity.HasOne(d => d.CityFinishIataCodeNavigation).WithMany(p => p.DictSubsidizedRouteCityFinishIataCodeNavigations)
            .HasForeignKey(d => d.CityFinishIataCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("dict_subsidized_routes_fkey_city_finish_iata_code");

        entity.HasOne(d => d.CityStartIataCodeNavigation).WithMany(p => p.DictSubsidizedRouteCityStartIataCodeNavigations)
            .HasForeignKey(d => d.CityStartIataCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("dict_subsidized_routes_fkey_city_start_iata_code");
    }
}