namespace PSP.Domain.Models;

/// <summary>
/// Аэропорты
/// </summary>
public class Airport
{
    /// <summary>
    /// Код IATA аэропорта
    /// </summary>
    public string IataCode { get; set; } = null!;

    /// <summary>
    /// Код ICAO аэропорта
    /// </summary>
    public string IcaoCode { get; set; } = null!;

    /// <summary>
    /// Код аэропорта по Воздушному кодексу РФ
    /// </summary>
    public string? RfCode { get; set; }

    /// <summary>
    /// Название аэропорта
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Ссылка на населенный пункт расположения аэропорта
    /// </summary>
    public string CityIataCode { get; set; } = null!;

    public virtual City CityIataCodeNavigation { get; set; } = null!;

    public virtual ICollection<FlightSegment> FlightSegmentArrivePlaceNavigations { get; set; } = new List<FlightSegment>();

    public virtual ICollection<FlightSegment> FlightSegmentDepartPlaceNavigations { get; set; } = new List<FlightSegment>();

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
