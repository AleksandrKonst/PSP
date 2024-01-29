using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_ticket_type_pkey");

        entity.ToTable("dict_ticket_types", "PSP", tb => tb.HasComment("Типы перевозочных документов"));

        entity.Property(e => e.Code)
            .ValueGeneratedNever()
            .HasComment("Код типа перевозочного документа")
            .HasColumnName("code");
        entity.Property(e => e.Type)
            .HasComment("Тип перевозочного документа")
            .HasColumnType("character varying")
            .HasColumnName("type");
    }
}