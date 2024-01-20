namespace PSP.Domain.Models;

/// <summary>
/// Населенные пункты
/// </summary>
public class City
{
    /// <summary>
    /// Код IATA населенного пункта
    /// </summary>
    public string IataCode { get; set; } = null!;

    /// <summary>
    /// Название населенного пункта
    /// </summary>
    public string Name { get; set; } = null!;

    public virtual ICollection<Airport> DictAirports { get; set; } = new List<Airport>();

    public virtual ICollection<SubsidizedRoute> DictSubsidizedRouteCityFinishIataCodeNavigations { get; set; } = new List<SubsidizedRoute>();

    public virtual ICollection<SubsidizedRoute> DictSubsidizedRouteCityStartIataCodeNavigations { get; set; } = new List<SubsidizedRoute>();
}
