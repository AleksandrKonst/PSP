using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context.EntityConfiguration;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_document_types_pkey");

        entity.ToTable("dict_document_types", "PSP", tb => tb.HasComment("Типы документов, удостоверяющих личность"));

        entity.Property(e => e.Code)
            .HasComment("Код типа документа, удостоверяющего личность")
            .HasColumnType("character varying")
            .HasColumnName("code");
        entity.Property(e => e.Type)
            .HasComment("Тип документа, удостоверяющего личность")
            .HasColumnType("character varying")
            .HasColumnName("type");
    }
}