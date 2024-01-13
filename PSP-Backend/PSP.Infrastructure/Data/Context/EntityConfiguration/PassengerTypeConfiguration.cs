using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class PassengerTypeConfiguration : IEntityTypeConfiguration<PassengerType>
{
    public void Configure(EntityTypeBuilder<PassengerType> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_passenger_types_pkey");

        entity.ToTable("dict_passenger_types", "PSP", tb => tb.HasComment("Типы пассажиров"));

        entity.Property(e => e.Code)
            .HasComment("Код типа пассажира")
            .HasColumnType("character varying")
            .HasColumnName("code");
        entity.Property(e => e.Appendices)
            .HasComment("Список номеров приложений к Постановлению Правительства РФ №215")
            .HasColumnName("appendices");
        entity.Property(e => e.Description)
            .HasComment("Описание типа пассажира")
            .HasColumnType("character varying")
            .HasColumnName("description");
        entity.Property(e => e.QuotaCategories)
            .HasComment("Список ссылок на категории квотирования")
            .HasColumnType("character varying[]")
            .HasColumnName("quota_categories");
        entity.Property(e => e.Type)
            .HasComment("Тип пассажира")
            .HasColumnType("character varying")
            .HasColumnName("type");
    }
}