using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Context.EntityConfiguration;

public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationType>
{
    public void Configure(EntityTypeBuilder<OperationType> entity)
    {
        entity.HasKey(e => e.Code).HasName("dict_operation_type_pk");

        entity.ToTable("dict_operation_type", "PSP");

        entity.Property(e => e.Code)
            .HasComment("Код операции")
            .HasColumnType("character varying")
            .HasColumnName("code");
        entity.Property(e => e.OperationDescription)
            .HasComment("Описание операции")
            .HasColumnType("character varying")
            .HasColumnName("operation_description");
    }
}