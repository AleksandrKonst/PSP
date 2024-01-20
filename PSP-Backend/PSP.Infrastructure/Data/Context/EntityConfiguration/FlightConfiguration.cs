using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_flight_pk");

        entity.ToTable("dict_flight", "PSP");

        entity.Property(e => e.Code)
            .ValueGeneratedNever()
            .HasComment("Код маршрута")
            .HasColumnName("code");
        entity.Property(e => e.AirlineCode)
            .HasComment("Код авиаперевочика")
            .HasColumnType("character varying")
            .HasColumnName("airline_code");
        entity.Property(e => e.ArrivePlace)
            .HasComment("Код аэропорта прилета")
            .HasColumnType("character varying")
            .HasColumnName("arrive_place");
        entity.Property(e => e.Currency)
            .HasComment("Валюта")
            .HasColumnType("character varying")
            .HasColumnName("currency");
        entity.Property(e => e.DepartPlace)
            .HasComment("Код аэропорта отправки")
            .HasColumnType("character varying")
            .HasColumnName("depart_place");
        entity.Property(e => e.PreferentialAmount)
            .HasComment("Колличество субсидий")
            .HasColumnName("preferential_amount");
        entity.Property(e => e.SubsitizedAmount)
            .HasComment("Стоимость льготного билета")
            .HasColumnName("subsitized_amount");

        entity.HasOne(d => d.AirlineCodeNavigation).WithMany(p => p.DictFlights)
            .HasForeignKey(d => d.AirlineCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("dict_flight_airline_fk");

        entity.HasOne(d => d.ArrivePlaceNavigation).WithMany(p => p.DictFlightArrivePlaceNavigations)
            .HasForeignKey(d => d.ArrivePlace)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("dict_flight_arrive_place_fk");

        entity.HasOne(d => d.DepartPlaceNavigation).WithMany(p => p.DictFlightDepartPlaceNavigations)
            .HasForeignKey(d => d.DepartPlace)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("dict_flight_depart_place_fk");
    }
}