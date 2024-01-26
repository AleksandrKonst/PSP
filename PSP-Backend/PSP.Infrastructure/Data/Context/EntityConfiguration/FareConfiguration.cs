using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class FareConfiguration : IEntityTypeConfiguration<Fare>
{
    public void Configure(EntityTypeBuilder<Fare> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_fare_pk");

            entity.ToTable("dict_fare", "PSP");

            entity.Property(e => e.Code)
                .HasComment("Идендификатор тарифа")
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.AirlineCode)
                .HasComment("Код авиакомпании")
                .HasColumnType("character varying")
                .HasColumnName("airline_code");
            entity.Property(e => e.Currency)
                .HasComment("Валюта")
                .HasColumnType("character varying")
                .HasColumnName("currency");
            entity.Property(e => e.Name)
                .HasComment("Название тарифа")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.PreferentialAmount)
                .HasComment("Колличество субсидий")
                .HasColumnName("preferential_amount");
            entity.Property(e => e.QuotaCategoryCode)
                .HasComment("Код категории квотирования")
                .HasColumnType("character varying")
                .HasColumnName("quota_category_code");
            entity.Property(e => e.SubsidizedRouteId)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор направления")
                .HasColumnName("subsidized_route_id");
            entity.Property(e => e.SubsitizedAmount)
                .HasComment("Стоимость льготного билета")
                .HasColumnName("subsitized_amount");

            entity.HasOne(d => d.AirlineCodeNavigation).WithMany(p => p.DictFares)
                .HasForeignKey(d => d.AirlineCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_fare_airline_fk");

            entity.HasOne(d => d.QutoaCategoryCodeNavigation).WithMany(p => p.DictFares)
                .HasForeignKey(d => d.QuotaCategoryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_fare_quota_category_fk");

            entity.HasOne(d => d.SubsidizedRoute).WithMany(p => p.DictFares)
                .HasForeignKey(d => d.SubsidizedRouteId)
                .HasConstraintName("dict_fare_sub_fk");
    }
}