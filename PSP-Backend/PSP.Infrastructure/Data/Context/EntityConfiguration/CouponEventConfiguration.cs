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
            entity.Property(e => e.DocumentNumber)
                .HasComment("Номер документа")
                .HasColumnType("character varying")
                .HasColumnName("document_number");
            entity.Property(e => e.DocumentNumberLatin)
                .HasComment("Номер документа latin")
                .HasColumnType("character varying")
                .HasColumnName("document_number_latin");
            entity.Property(e => e.DocumentTypeCode)
                .HasComment("Тип документа")
                .HasColumnType("character varying")
                .HasColumnName("document_type_code");
            entity.Property(e => e.FlightCode)
                .HasComment("Код перелета")
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
            entity.Property(e => e.QuotaCode)
                .HasComment("Код квотирования")
                .HasColumnType("character varying")
                .HasColumnName("quota_code");
            entity.Property(e => e.TicketNumber)
                .HasComment("Номер билета")
                .HasColumnType("character varying")
                .HasColumnName("ticket_number");
            entity.Property(e => e.TicketType)
                .HasComment("Тип билета")
                .HasColumnName("ticket_type");

            entity.HasOne(d => d.DocumentTypeCodeNavigation).WithMany(p => p.CouponEvents)
                .HasForeignKey(d => d.DocumentTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_document_type_fk");

            entity.HasOne(d => d.FlightCodeNavigation).WithMany(p => p.CouponEvents)
                .HasForeignKey(d => d.FlightCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_flight_fk");

            entity.HasOne(d => d.OperationTypeNavigation).WithMany(p => p.CouponEvents)
                .HasForeignKey(d => d.OperationType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_operation_type_fk");

            entity.HasOne(d => d.Passenger).WithMany(p => p.CouponEvents)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_passenger_fk");

            entity.HasOne(d => d.QuotaCodeNavigation).WithMany(p => p.CouponEvents)
                .HasForeignKey(d => d.QuotaCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_quota_fk");

            entity.HasOne(d => d.TicketTypeNavigation).WithMany(p => p.CouponEvents)
                .HasForeignKey(d => d.TicketType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_ticket_type_fk");
    }
}