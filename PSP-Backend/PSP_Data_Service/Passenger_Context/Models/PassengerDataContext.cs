using Microsoft.EntityFrameworkCore;

namespace PSP_Data_Service.Passenger_Context.Models;

public class PassengerDataContext : DbContext
{
    public PassengerDataContext(DbContextOptions<PassengerDataContext> options) : base(options)
    {
    }

    public virtual DbSet<PassengerQuotaCount> ConPassengerQuotaCounts { get; set; }

    public virtual DbSet<Passenger> DataPassengers { get; set; }
    
    public virtual DbSet<DocumentType> DictDocumentTypes { get; set; }

    public virtual DbSet<GenderType> DictGenders { get; set; }

    public virtual DbSet<PassengerType> DictPassengerTypes { get; set; }

    public virtual DbSet<QuotaCategory> DictQuotaCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost; Port=5432;Database=PSP_Data;User Id=postgres;Password=1243");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PassengerQuotaCount>(entity =>
        {
            entity.HasKey(e => new { e.PassengerId, e.QuotaCategoriesCode, e.QuotaYear }).HasName("data_passenger_quota_count_pk");

            entity.ToTable("con_passenger_quota_count", "PSP");

            entity.Property(e => e.PassengerId)
                .HasDefaultValueSql("nextval('\"PSP\".data_passenger_quota_count_passenger_id_seq'::regclass)")
                .HasComment("Идендификатор пассажира")
                .HasColumnName("passenger_id");
            entity.Property(e => e.QuotaCategoriesCode)
                .HasComment("Код категории квотирования")
                .HasColumnType("character varying")
                .HasColumnName("quota_categories_code");
            entity.Property(e => e.QuotaYear)
                .HasComment("Год квотирования")
                .HasColumnType("character varying")
                .HasColumnName("quota_year");
            entity.Property(e => e.AvailableCount)
                .HasComment("Колличество доступных квот")
                .HasColumnName("available_count");
            entity.Property(e => e.IssuedCount)
                .HasComment("Колличество оформленных билетов")
                .HasColumnName("issued_count");
            entity.Property(e => e.RefundCount)
                .HasComment("Колличество возращенных билетов")
                .HasColumnName("refund_count");
            entity.Property(e => e.UsedCount)
                .HasComment("Колличество использованых билетов")
                .HasColumnName("used_count");

            entity.HasOne(d => d.Passenger).WithMany(p => p.ConPassengerQuotaCounts)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_passenger_quota_count_passenger_fk");

            entity.HasOne(d => d.QuotaCategoriesCodeNavigation).WithMany(p => p.ConPassengerQuotaCounts)
                .HasForeignKey(d => d.QuotaCategoriesCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_passenger_quota_count_category_fk");
        });
        
        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("data_passenger_pk");

            entity.ToTable("data_passenger", "PSP");

            entity.Property(e => e.Id)
                .HasComment("Идентификатор пассажира")
                .HasColumnName("id");
            entity.Property(e => e.Birthdate)
                .HasComment("Дата рождения")
                .HasColumnName("birthdate");
            entity.Property(e => e.DocumentNumber)
                .HasComment("Серия документа")
                .HasColumnType("character varying")
                .HasColumnName("document_number");
            entity.Property(e => e.DocumentNumbersLatin)
                .HasComment("Разлиные варианты названий документов")
                .HasColumnType("character varying[]")
                .HasColumnName("document_numbers_latin");
            entity.Property(e => e.DocumentTypeCode)
                .HasComment("Тип документа")
                .HasColumnType("character varying")
                .HasColumnName("document_type_code");
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

            entity.HasOne(d => d.DocumentTypeCodeNavigation).WithMany(p => p.DataPassengers)
                .HasForeignKey(d => d.DocumentTypeCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_passenger_document_type_fk");

            entity.HasOne(d => d.GenderTypeNavigation).WithMany(p => p.DataPassengers)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_passenger_gender_fk");
        });
        
        modelBuilder.Entity<DocumentType>(entity =>
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
        });

        modelBuilder.Entity<GenderType>(entity =>
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
        });

        modelBuilder.Entity<PassengerType>(entity =>
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
        });

        modelBuilder.Entity<QuotaCategory>(entity =>
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
        });
    }
}