namespace Domain.Models;

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
    
    /// <summary>
    /// Широта
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Долгота
    /// </summary>
    public decimal? Longitude { get; set; }

    public virtual City CityIataCodeNavigation { get; set; } = null!;

    public virtual ICollection<Flight> FlightArrivePlaceNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightDepartPlaceNavigations { get; set; } = new List<Flight>();
}
