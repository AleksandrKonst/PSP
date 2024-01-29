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
        entity.Property(e => e.Amount)
            .HasComment("Стоимость тарифа")
            .HasColumnName("amount");
        entity.Property(e => e.Currency)
            .HasComment("Валюта")
            .HasColumnType("character varying")
            .HasColumnName("currency");
        entity.Property(e => e.Special)
            .HasComment("Специальный тариф")
            .HasColumnName("special");
    }
}