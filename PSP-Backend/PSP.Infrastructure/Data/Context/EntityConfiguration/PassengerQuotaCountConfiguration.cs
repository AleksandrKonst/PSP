using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class PassengerQuotaCountConfiguration : IEntityTypeConfiguration<PassengerQuotaCount>
{
    public void Configure(EntityTypeBuilder<PassengerQuotaCount> entity)
    {
        entity.HasKey(e => new { e.PassengerId, e.QuotaCategoriesCode, e.QuotaYear }).HasName("data_passenger_quota_count_pk");

        entity.ToTable("con_passenger_quota_count", "PSP");

        entity.Property(e => e.PassengerId)
            .HasDefaultValueSql("nextval('\"PSP\".data_passenger_quota_count_passenger_id_seq'::regclass)")
            .HasComment("Идендификатор пассажира")
            .HasColumnName("passenger_id");
        entity.Property(e => e.QuotaCategoriesCode)
            .HasComment("Код категории квотирования")
            .HasColumnType("character varying")
            .HasColumnName("quota_categories_code");
        entity.Property(e => e.QuotaYear)
            .HasComment("Год квотирования")
            .HasColumnType("character varying")
            .HasColumnName("quota_year");
        entity.Property(e => e.AvailableCount)
            .HasComment("Колличество доступных квот")
            .HasColumnName("available_count");
        entity.Property(e => e.IssuedCount)
            .HasComment("Колличество оформленных билетов")
            .HasColumnName("issued_count");
        entity.Property(e => e.RefundCount)
            .HasComment("Колличество возращенных билетов")
            .HasColumnName("refund_count");
        entity.Property(e => e.UsedCount)
            .HasComment("Колличество использованых билетов")
            .HasColumnName("used_count");

        entity.HasOne(d => d.Passenger).WithMany(p => p.ConPassengerQuotaCounts)
            .HasForeignKey(d => d.PassengerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_passenger_quota_count_passenger_fk");

        entity.HasOne(d => d.QuotaCategoriesCodeNavigation).WithMany(p => p.ConPassengerQuotaCounts)
            .HasForeignKey(d => d.QuotaCategoriesCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_passenger_quota_count_category_fk");
    }
}