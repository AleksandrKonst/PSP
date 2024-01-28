using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.Domain.Models;

namespace PSP.Infrastructure.Data.Context.EntityConfiguration;

public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> entity)
    {
        entity.HasKey(e => e.Id).HasName("data_passenger_pk");

        entity.ToTable("data_passenger", "PSP");

        entity.Property(e => e.Id)
            .HasComment("Идентификатор пассажира")
            .HasColumnName("id");
        entity.Property(e => e.Birthdate)
            .HasComment("Дата рождения")
            .HasColumnName("birthdate");
        entity.Property(e => e.Gender)
            .HasComment("Пол пассажира")
            .HasColumnType("character varying")
            .HasColumnName("gender");
        entity.Property(e => e.Name)
            .HasComment("Имя пассажира")
            .HasColumnType("character varying")
            .HasColumnName("name");
        entity.Property(e => e.PassengerTypes)
            .HasComment("Типы пассажира")
            .HasColumnType("character varying[]")
            .HasColumnName("passenger_types");
        entity.Property(e => e.Patronymic)
            .HasComment("Отчество пассажира")
            .HasColumnType("character varying")
            .HasColumnName("patronymic");
        entity.Property(e => e.Surname)
            .HasComment("Фамилия пассажира")
            .HasColumnType("character varying")
            .HasColumnName("surname");

        entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Passengers)
            .HasForeignKey(d => d.Gender)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("data_passenger_gender_fk");
    }
}