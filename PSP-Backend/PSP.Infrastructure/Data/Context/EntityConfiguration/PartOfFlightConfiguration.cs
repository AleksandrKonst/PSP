using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class PartOfFlightConfiguration : IEntityTypeConfiguration<PartOfFlight>
{
    public void Configure(EntityTypeBuilder<PartOfFlight> entity)
    {
        entity.HasKey(e => new { e.FlightCode, e.FlightPart }).HasName("data_flight_parts_pk");

        entity.ToTable("con_flight_parts", "PSP");

        entity.Property(e => e.FlightCode)
            .HasComment("Код маршрута")
            .HasColumnName("flight_code");
        entity.Property(e => e.FlightPart)
            .HasComment("Номер части маршрута")
            .HasColumnName("flight_part");
        entity.Property(e => e.ArrivePlace)
            .HasComment("Аэропорт прилета")
            .HasColumnType("character varying")
            .HasColumnName("arrive_place");
        entity.Property(e => e.DepartPlace)
            .HasComment("Аэрпорт отлета")
            .HasColumnType("character varying")
            .HasColumnName("depart_place");
        entity.Property(e => e.FlightArriveDatetime)
            .HasComment("Время прилета")
            .HasColumnName("flight_arrive_datetime");
        entity.Property(e => e.FlightDepartDatetime)
            .HasComment("Время отлета")
            .HasColumnName("flight_depart_datetime");

        entity.HasOne(d => d.ArrivePlaceNavigation).WithMany(p => p.ConFlightPartArrivePlaceNavigations)
            .HasForeignKey(d => d.ArrivePlace)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_flight_parts_arrive_place_fk");

        entity.HasOne(d => d.DepartPlaceNavigation).WithMany(p => p.ConFlightPartDepartPlaceNavigations)
            .HasForeignKey(d => d.DepartPlace)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_flight_parts_depart_place_fk");

        entity.HasOne(d => d.FlightCodeNavigation).WithMany(p => p.ConFlightParts)
            .HasForeignKey(d => d.FlightCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_flight_parts_flight_fk");
    }
}