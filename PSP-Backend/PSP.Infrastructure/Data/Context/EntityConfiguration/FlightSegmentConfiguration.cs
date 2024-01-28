using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class FlightSegmentConfiguration : IEntityTypeConfiguration<FlightSegment>
{
    public void Configure(EntityTypeBuilder<FlightSegment> entity)
    {
        entity.HasKey(e => new { e.FlightCode, e.FlightPart }).HasName("data_flight_parts_pk");

        entity.ToTable("data_flight_segment", "PSP");

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

        entity.HasOne(d => d.ArrivePlaceNavigation).WithMany(p => p.FlightSegmentArrivePlaceNavigations)
            .HasForeignKey(d => d.ArrivePlace)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_flight_parts_arrive_place_fk");

        entity.HasOne(d => d.DepartPlaceNavigation).WithMany(p => p.FlightSegmentDepartPlaceNavigations)
            .HasForeignKey(d => d.DepartPlace)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_flight_parts_depart_place_fk");

        entity.HasOne(d => d.FlightCodeNavigation).WithMany(p => p.FlightSegments)
            .HasForeignKey(d => d.FlightCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_flight_parts_flight_fk");
    }
}