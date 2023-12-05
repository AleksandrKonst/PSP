using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Data.Models;

namespace PSP_Data_Service.Data;

public partial class PspDataContext : DbContext
{
    public PspDataContext()
    {
    }

    public PspDataContext(DbContextOptions<PspDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConFlightPart> ConFlightParts { get; set; }

    public virtual DbSet<ConPassengerQuotaCount> ConPassengerQuotaCounts { get; set; }

    public virtual DbSet<DataCouponEvent> DataCouponEvents { get; set; }

    public virtual DbSet<DataPassenger> DataPassengers { get; set; }

    public virtual DbSet<DictAirline> DictAirlines { get; set; }

    public virtual DbSet<DictAirport> DictAirports { get; set; }

    public virtual DbSet<DictCity> DictCities { get; set; }

    public virtual DbSet<DictDocumentType> DictDocumentTypes { get; set; }

    public virtual DbSet<DictFlight> DictFlights { get; set; }

    public virtual DbSet<DictGender> DictGenders { get; set; }

    public virtual DbSet<DictPassengerType> DictPassengerTypes { get; set; }

    public virtual DbSet<DictQuotaCategory> DictQuotaCategories { get; set; }

    public virtual DbSet<DictSubsidizedRoute> DictSubsidizedRoutes { get; set; }

    public virtual DbSet<DictTicketType> DictTicketTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost; Port=5432;Database=PSP_Data;User Id=postgres;Password=1243");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConFlightPart>(entity =>
        {
            entity.HasKey(e => new { e.FlightCode, e.FlightPart }).HasName("data_flight_parts_pk");

            entity.ToTable("con_flight_parts", "PSP");

            entity.Property(e => e.FlightCode)
                .HasComment("Код маршрута")
                .HasColumnName("flight_code");
            entity.Property(e => e.FlightPart)
                .HasComment("Номер части маршрута")
                .HasColumnName("flight_part");
            entity.Property(e => e.ArrivePlace)
                .HasComment("Аэропорт прилета")
                .HasColumnType("character varying")
                .HasColumnName("arrive_place");
            entity.Property(e => e.DepartPlace)
                .HasComment("Аэрпорт отлета")
                .HasColumnType("character varying")
                .HasColumnName("depart_place");
            entity.Property(e => e.FlightArriveDatetime)
                .HasComment("Время прилета")
                .HasColumnName("flight_arrive_datetime");
            entity.Property(e => e.FlightDepartDatetime)
                .HasComment("Время отлета")
                .HasColumnName("flight_depart_datetime");

            entity.HasOne(d => d.ArrivePlaceNavigation).WithMany(p => p.ConFlightPartArrivePlaceNavigations)
                .HasForeignKey(d => d.ArrivePlace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_flight_parts_arrive_place_fk");

            entity.HasOne(d => d.DepartPlaceNavigation).WithMany(p => p.ConFlightPartDepartPlaceNavigations)
                .HasForeignKey(d => d.DepartPlace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_flight_parts_depart_place_fk");

            entity.HasOne(d => d.FlightCodeNavigation).WithMany(p => p.ConFlightParts)
                .HasForeignKey(d => d.FlightCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_flight_parts_flight_fk");
        });

        modelBuilder.Entity<ConPassengerQuotaCount>(entity =>
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

        modelBuilder.Entity<DataCouponEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("data_coupon_events_pk");

            entity.ToTable("data_coupon_events", "PSP", tb => tb.HasComment("События с купонами"));

            entity.Property(e => e.Id)
                .HasComment("Идентификатор события с купоном")
                .HasColumnName("id");
            entity.Property(e => e.FlightCode)
                .HasComment("Код маршрута")
                .HasColumnName("flight_code");
            entity.Property(e => e.OperationDatetimeTimezone)
                .HasComment("Временная зона времени операции с билетами")
                .HasColumnName("operation_datetime_timezone");
            entity.Property(e => e.OperationDatetimeUtc)
                .HasComment("Дата и время операции с билетами (UTC)")
                .HasColumnName("operation_datetime_utc");
            entity.Property(e => e.OperationPlace)
                .HasComment("Место проведения операции с билетами")
                .HasColumnType("character varying")
                .HasColumnName("operation_place");
            entity.Property(e => e.OperationType)
                .HasComment("Тип операции с билетами")
                .HasColumnType("character varying")
                .HasColumnName("operation_type");
            entity.Property(e => e.PassengerId)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор пассажира")
                .HasColumnName("passenger_id");
            entity.Property(e => e.QuotaCategoryCode)
                .HasComment("Код категории квотирования")
                .HasColumnType("character varying")
                .HasColumnName("quota_category_code");
            entity.Property(e => e.SubsidizedRouteId)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор субсидируемого маршрута")
                .HasColumnName("subsidized_route_id");
            entity.Property(e => e.TicketType)
                .HasComment("Тип билета")
                .HasColumnName("ticket_type");

            entity.HasOne(d => d.FlightCodeNavigation).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.FlightCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_flights_fk");

            entity.HasOne(d => d.Passenger).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.PassengerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_passenger_fk");

            entity.HasOne(d => d.QuotaCategoryCodeNavigation).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.QuotaCategoryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_category_fk");

            entity.HasOne(d => d.SubsidizedRoute).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.SubsidizedRouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_subsidized_route_fk");

            entity.HasOne(d => d.TicketTypeNavigation).WithMany(p => p.DataCouponEvents)
                .HasForeignKey(d => d.TicketType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_coupon_events_ticket_fk");
        });

        modelBuilder.Entity<DataPassenger>(entity =>
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

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.DataPassengers)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("data_passenger_gender_fk");
        });

        modelBuilder.Entity<DictAirline>(entity =>
        {
            entity.HasKey(e => e.IataCode).HasName("dict_airlines_pkey");

            entity.ToTable("dict_airlines", "PSP", tb => tb.HasComment("Авиакомпании"));

            entity.Property(e => e.IataCode)
                .HasComment("Код IATA авиакомпании")
                .HasColumnType("character varying")
                .HasColumnName("iata_code");
            entity.Property(e => e.Country)
                .HasComment("Страна авиакомпании")
                .HasColumnType("character varying")
                .HasColumnName("country");
            entity.Property(e => e.IcaoCode)
                .HasComment("Код ICAO авиакомпании")
                .HasColumnType("character varying")
                .HasColumnName("icao_code");
            entity.Property(e => e.NameEn)
                .HasComment("Название авиакомпании (англ.)")
                .HasColumnType("character varying")
                .HasColumnName("name_en");
            entity.Property(e => e.NameRu)
                .HasComment("Название авиакомпании (рус.)")
                .HasColumnType("character varying")
                .HasColumnName("name_ru");
            entity.Property(e => e.ReportsUseFirstTransferFlightDepartDate)
                .HasComment("Авиакомпания отбирает в отчеты в Росавиацию трансферные перевозки по дате первого рейса")
                .HasColumnName("reports_use_first_transfer_flight_depart_date");
            entity.Property(e => e.ReportsUseFlightDataFact)
                .HasComment("Авиакомпания сдает отчеты в Росавиацию по фактическим данным рейсов")
                .HasColumnName("reports_use_flight_data_fact");
            entity.Property(e => e.RfCode)
                .HasComment("Код авиакомпании по Воздушному кодексу РФ")
                .HasColumnType("character varying")
                .HasColumnName("rf_code");
            entity.Property(e => e.TransportationPeriods)
                .HasComment("Список периодов выполнения авиакомпанией субсидированных перевозок")
                .HasColumnType("tsmultirange")
                .HasColumnName("transportation_periods");
        });

        modelBuilder.Entity<DictAirport>(entity =>
        {
            entity.HasKey(e => e.IataCode).HasName("dict_airports_pkey");

            entity.ToTable("dict_airports", "PSP", tb => tb.HasComment("Аэропорты"));

            entity.Property(e => e.IataCode)
                .HasComment("Код IATA аэропорта")
                .HasColumnType("character varying")
                .HasColumnName("iata_code");
            entity.Property(e => e.CityIataCode)
                .HasComment("Ссылка на населенный пункт расположения аэропорта")
                .HasColumnType("character varying")
                .HasColumnName("city_iata_code");
            entity.Property(e => e.IcaoCode)
                .HasComment("Код ICAO аэропорта")
                .HasColumnType("character varying")
                .HasColumnName("icao_code");
            entity.Property(e => e.Name)
                .HasComment("Название аэропорта")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.RfCode)
                .HasComment("Код аэропорта по Воздушному кодексу РФ")
                .HasColumnType("character varying")
                .HasColumnName("rf_code");

            entity.HasOne(d => d.CityIataCodeNavigation).WithMany(p => p.DictAirports)
                .HasForeignKey(d => d.CityIataCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_airports_fkey_city_iata_code");
        });

        modelBuilder.Entity<DictCity>(entity =>
        {
            entity.HasKey(e => e.IataCode).HasName("dict_cities_pkey");

            entity.ToTable("dict_cities", "PSP", tb => tb.HasComment("Населенные пункты"));

            entity.Property(e => e.IataCode)
                .HasComment("Код IATA населенного пункта")
                .HasColumnType("character varying")
                .HasColumnName("iata_code");
            entity.Property(e => e.Name)
                .HasComment("Название населенного пункта")
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<DictDocumentType>(entity =>
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

        modelBuilder.Entity<DictFlight>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("dict_flight_pk");

            entity.ToTable("dict_flight", "PSP");

            entity.Property(e => e.Code)
                .ValueGeneratedNever()
                .HasComment("Код маршрута")
                .HasColumnName("code");
            entity.Property(e => e.AirlineCode)
                .HasComment("Код авиаперевочика")
                .HasColumnType("character varying")
                .HasColumnName("airline_code");
            entity.Property(e => e.ArrivePlace)
                .HasComment("Код аэропорта прилета")
                .HasColumnType("character varying")
                .HasColumnName("arrive_place");
            entity.Property(e => e.Currency)
                .HasComment("Валюта")
                .HasColumnType("character varying")
                .HasColumnName("currency");
            entity.Property(e => e.DepartPlace)
                .HasComment("Код аэропорта отправки")
                .HasColumnType("character varying")
                .HasColumnName("depart_place");
            entity.Property(e => e.PreferentialAmount)
                .HasComment("Колличество субсидий")
                .HasColumnName("preferential_amount");
            entity.Property(e => e.SubsitizedAmount)
                .HasComment("Стоимость льготного билета")
                .HasColumnName("subsitized_amount");

            entity.HasOne(d => d.AirlineCodeNavigation).WithMany(p => p.DictFlights)
                .HasForeignKey(d => d.AirlineCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_flight_airline_fk");

            entity.HasOne(d => d.ArrivePlaceNavigation).WithMany(p => p.DictFlightArrivePlaceNavigations)
                .HasForeignKey(d => d.ArrivePlace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_flight_arrive_place_fk");

            entity.HasOne(d => d.DepartPlaceNavigation).WithMany(p => p.DictFlightDepartPlaceNavigations)
                .HasForeignKey(d => d.DepartPlace)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_flight_depart_place_fk");
        });

        modelBuilder.Entity<DictGender>(entity =>
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

        modelBuilder.Entity<DictPassengerType>(entity =>
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

        modelBuilder.Entity<DictQuotaCategory>(entity =>
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

        modelBuilder.Entity<DictSubsidizedRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dict_subsidized_routes_pkey");

            entity.ToTable("dict_subsidized_routes", "PSP", tb => tb.HasComment("Субсидированные направления"));

            entity.Property(e => e.Id)
                .HasComment("Идентификатор субсидированного направления")
                .HasColumnName("id");
            entity.Property(e => e.Appendix)
                .HasComment("Номер приложения к Постановлению Правительства РФ №215")
                .HasColumnName("appendix");
            entity.Property(e => e.CityFinishIataCode)
                .HasComment("Ссылка на конечный населенный пункт направления")
                .HasColumnType("character varying")
                .HasColumnName("city_finish_iata_code");
            entity.Property(e => e.CityStartIataCode)
                .HasComment("Ссылка на начальный населенный пункт направления")
                .HasColumnType("character varying")
                .HasColumnName("city_start_iata_code");
            entity.Property(e => e.Currency)
                .HasComment("Валюта")
                .HasColumnType("character varying")
                .HasColumnName("currency");
            entity.Property(e => e.FareAmount)
                .HasComment("Размер специального тарифа для направления")
                .HasColumnName("fare_amount");
            entity.Property(e => e.InteriorCities)
                .HasComment("Список промежуточных населенных пунктов направления")
                .HasColumnType("character varying[]")
                .HasColumnName("interior_cities");
            entity.Property(e => e.SubsidyAmount)
                .HasComment("Размер субсидии для направления")
                .HasColumnName("subsidy_amount");
            entity.Property(e => e.ValidityFrom)
                .HasComment("Начало периода актуальности записи")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("validity_from");
            entity.Property(e => e.ValidityTo)
                .HasComment("Окончание периода актуальности записи")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("validity_to");

            entity.HasOne(d => d.CityFinishIataCodeNavigation).WithMany(p => p.DictSubsidizedRouteCityFinishIataCodeNavigations)
                .HasForeignKey(d => d.CityFinishIataCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_subsidized_routes_fkey_city_finish_iata_code");

            entity.HasOne(d => d.CityStartIataCodeNavigation).WithMany(p => p.DictSubsidizedRouteCityStartIataCodeNavigations)
                .HasForeignKey(d => d.CityStartIataCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dict_subsidized_routes_fkey_city_start_iata_code");
        });

        modelBuilder.Entity<DictTicketType>(entity =>
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
