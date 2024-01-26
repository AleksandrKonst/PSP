using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class CouponEventConfiguration : IEntityTypeConfiguration<CouponEvent>
{
    public void Configure(EntityTypeBuilder<CouponEvent> entity)
    {
        entity.HasKey(e => e.Id).HasName("data_coupon_events_pk");

            entity.ToTable("data_coupon_events", "PSP", tb => tb.HasComment("События с купонами"));

            entity.Property(e => e.Id)
                .HasComment("Идентификатор события с купоном")
                .HasColumnName("id");
            entity.Property(e => e.FareCode)
                .HasColumnType("character varying")
                .HasColumnName("fare_code");
            entity.Property(e => e.FlightCode)
                .HasComment("Код маршрута")
                .HasColumnName("flight_code");
            entity.Property(e => e.OperationDatetimeTimezone)
                .HasComment("Временная зона времени операции с билетами")
                .HasColumnName("operation_datetime_timezone");
            entity.Property(e => e.OperationDatetimeUtc)
                .HasComment("Дата и время операции с билетами (UTC)")
                .HasColumnName("operation_datetime_utc");
            entity.Property(e => e.OperationPlace)
                .HasComment("Место проведения операции с билетами")
                .HasColumnType("character varying")
                .HasColumnName("operation_place");
            entity.Property(e => e.OperationType)
                .HasComment("Тип операции с билетами")
                .HasColumnType("character varying")
                .HasColumnName("operation_type");
            entity.Property(e => e.PassengerId)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор пассажира")
                .HasColumnName("passenger_id");
            entity.Property(e => e.TicketType)
                .HasComment("Тип билета")
                .HasColumnName("ticket_type");

            entity.HasOne(d => d.Fare).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.FareCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_fare_fk");

            entity.HasOne(d => d.Flight).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.FlightCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_flight_fk");

            entity.HasOne(d => d.Passenger).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_passenger_fk");

            entity.HasOne(d => d.TicketTypeNavigation).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.TicketType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_ticket_fk");
    }
}