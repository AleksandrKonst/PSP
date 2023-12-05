namespace PSP_Data_Service.Data.Models;

/// <summary>
/// Аэропорты
/// </summary>
public partial class DictAirport
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

    public virtual DictCity CityIataCodeNavigation { get; set; } = null!;

    public virtual ICollection<ConFlightPart> ConFlightPartArrivePlaceNavigations { get; set; } = new List<ConFlightPart>();

    public virtual ICollection<ConFlightPart> ConFlightPartDepartPlaceNavigations { get; set; } = new List<ConFlightPart>();

    public virtual ICollection<DictFlight> DictFlightArrivePlaceNavigations { get; set; } = new List<DictFlight>();

    public virtual ICollection<DictFlight> DictFlightDepartPlaceNavigations { get; set; } = new List<DictFlight>();
}
