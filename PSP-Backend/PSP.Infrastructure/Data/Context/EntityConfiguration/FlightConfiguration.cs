using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_flight_pk");

            entity.ToTable("data_flight", "PSP");

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasComment("Код маршрута")
                .HasColumnName("code");
            entity.Property(e => e.AirlineCode)
                .HasComment("Код авиаперевочика")
                .HasColumnType("character varying")
                .HasColumnName("airline_code");
            entity.Property(e => e.ArriveDatetimePlan)
                .HasComment("Время посадки")
                .HasColumnName("arrive_datetime_plan");
            entity.Property(e => e.ArrivePlace)
                .HasComment("Код аэропорта посадки")
                .HasColumnType("character varying")
                .HasColumnName("arrive_place");
            entity.Property(e => e.DepartDatetimePlan)
                .HasComment("Время отправки")
                .HasColumnName("depart_datetime_plan");
            entity.Property(e => e.DepartPlace)
                .HasComment("Код аэропорта отправки")
                .HasColumnType("character varying")
                .HasColumnName("depart_place");
            entity.Property(e => e.FareCode)
                .HasComment("Код тарифа")
                .HasColumnType("character varying")
                .HasColumnName("fare_code");
            entity.Property(e => e.PnrCode)
                .HasComment("PNR код")
                .HasColumnType("character varying")
                .HasColumnName("pnr_code");

            entity.HasOne(d => d.AirlineCodeNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirlineCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_flight_airline_fk");

            entity.HasOne(d => d.ArrivePlaceNavigation).WithMany(p => p.FlightArrivePlaceNavigations)
                .HasForeignKey(d => d.ArrivePlace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_flight_arrive_place_fk");

            entity.HasOne(d => d.DepartPlaceNavigation).WithMany(p => p.FlightDepartPlaceNavigations)
                .HasForeignKey(d => d.DepartPlace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_flight_depart_place_fk");

            entity.HasOne(d => d.FareCodeNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.FareCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_flight_fk");
    }
}