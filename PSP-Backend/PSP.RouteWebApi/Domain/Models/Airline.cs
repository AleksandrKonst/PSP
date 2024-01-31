namespace Domain.Models;

/// <summary>
/// Авиакомпании
/// </summary>
public class Airline
{
    /// <summary>
    /// Код IATA авиакомпании
    /// </summary>
    public string IataCode { get; set; } = null!;

    /// <summary>
    /// Название авиакомпании (рус.)
    /// </summary>
    public string NameRu { get; set; } = null!;

    /// <summary>
    /// Название авиакомпании (англ.)
    /// </summary>
    public string NameEn { get; set; } = null!;

    /// <summary>
    /// Код ICAO авиакомпании
    /// </summary>
    public string IcaoCode { get; set; } = null!;

    /// <summary>
    /// Код авиакомпании по Воздушному кодексу РФ
    /// </summary>
    public string? RfCode { get; set; }

    /// <summary>
    /// Страна авиакомпании
    /// </summary>
    public string Country { get; set; } = null!;

    /// <summary>
    /// Авиакомпания сдает отчеты в Росавиацию по фактическим данным рейсов
    /// </summary>
    public bool ReportsUseFlightDataFact { get; set; }

    /// <summary>
    /// Авиакомпания отбирает в отчеты в Росавиацию трансферные перевозки по дате первого рейса
    /// </summary>
    public bool ReportsUseFirstTransferFlightDepartDate { get; set; }
    
    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
