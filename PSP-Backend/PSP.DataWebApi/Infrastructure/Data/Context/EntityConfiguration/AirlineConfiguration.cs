using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context.EntityConfiguration;

public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
{
    public void Configure(EntityTypeBuilder<Airline> entity)
    {
        entity.HasKey(e => e.IataCode).HasName("dict_airlines_pkey");

            entity.ToTable("dict_airlines", "PSP", tb => tb.HasComment("Авиакомпании"));

            entity.Property(e => e.IataCode)
                .HasComment("Код IATA авиакомпании")
                .HasColumnType("character varying")
                .HasColumnName("iata_code");
            entity.Property(e => e.Country)
                .HasComment("Страна авиакомпании")
                .HasColumnType("character varying")
                .HasColumnName("country");
            entity.Property(e => e.IcaoCode)
                .HasComment("Код ICAO авиакомпании")
                .HasColumnType("character varying")
                .HasColumnName("icao_code");
            entity.Property(e => e.NameEn)
                .HasComment("Название авиакомпании (англ.)")
                .HasColumnType("character varying")
                .HasColumnName("name_en");
            entity.Property(e => e.NameRu)
                .HasComment("Название авиакомпании (рус.)")
                .HasColumnType("character varying")
                .HasColumnName("name_ru");
            entity.Property(e => e.ReportsUseFirstTransferFlightDepartDate)
                .HasComment("Авиакомпания отбирает в отчеты в Росавиацию трансферные перевозки по дате первого рейса")
                .HasColumnName("reports_use_first_transfer_flight_depart_date");
            entity.Property(e => e.ReportsUseFlightDataFact)
                .HasComment("Авиакомпания сдает отчеты в Росавиацию по фактическим данным рейсов")
                .HasColumnName("reports_use_flight_data_fact");
            entity.Property(e => e.RfCode)
                .HasComment("Код авиакомпании по Воздушному кодексу РФ")
                .HasColumnType("character varying")
                .HasColumnName("rf_code");
    }
}