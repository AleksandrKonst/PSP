using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class OperationTypeConfiguration : IEntityTypeConfiguration<OperationType>
{
    public void Configure(EntityTypeBuilder<OperationType> entity)
    {
        entity.HasKey(e => e.OperationCode).HasName("dict_operation_type_pk");

        entity.ToTable("dict_operation_type", "PSP");

        entity.Property(e => e.OperationCode)
            .HasComment("Код операции")
            .HasColumnType("character varying")
            .HasColumnName("operation_code");
        entity.Property(e => e.OperationDescription)
            .HasComment("Описание операции")
            .HasColumnType("character varying")
            .HasColumnName("operation_description");
    }
}