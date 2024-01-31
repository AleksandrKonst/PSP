using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context.EntityConfiguration;

public class QuotaCategoryConfiguration : IEntityTypeConfiguration<QuotaCategory>
{
    public void Configure(EntityTypeBuilder<QuotaCategory> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_quota_categories_pkey");

        entity.ToTable("dict_quota_categories", "PSP", tb => tb.HasComment("Категории квотирования"));

        entity.Property(e => e.Code)
            .HasComment("Код категории квотирования")
            .HasColumnType("character varying")
            .HasColumnName("code");
        entity.Property(e => e.Appendices)
            .HasComment("Список номеров приложения к Постановлению Правительства РФ №215")
            .HasColumnName("appendices");
        entity.Property(e => e.Category)
            .HasComment("Категория квотирования")
            .HasColumnType("character varying")
            .HasColumnName("category");
        entity.Property(e => e.OneWayQuota)
            .HasComment("Квота в одном направлении")
            .HasColumnName("one_way_quota");
        entity.Property(e => e.RoundTripQuota)
            .HasComment("Квота в направлении туда и обратно")
            .HasColumnName("round_trip_quota");
    }
}