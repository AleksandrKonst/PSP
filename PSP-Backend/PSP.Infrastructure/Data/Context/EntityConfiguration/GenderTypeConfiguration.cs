using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class GenderTypeConfiguration : IEntityTypeConfiguration<GenderType>
{
    public void Configure(EntityTypeBuilder<GenderType> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_genders_pkey");

        entity.ToTable("dict_genders", "PSP", tb => tb.HasComment("Полы"));

        entity.Property(e => e.Code)
            .HasComment("Код пола")
            .HasColumnType("character varying")
            .HasColumnName("code");
        entity.Property(e => e.Gender)
            .HasComment("Пол")
            .HasColumnType("character varying")
            .HasColumnName("gender");
    }
}