namespace Domain.Models;

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

    public virtual ICollection<Airport> Airports { get; set; } = new List<Airport>();

    public virtual ICollection<SubsidizedRoute> SubsidizedRouteCityFinishIataCodeNavigations { get; set; } = new List<SubsidizedRoute>();

    public virtual ICollection<SubsidizedRoute> SubsidizedRouteCityStartIataCodeNavigations { get; set; } = new List<SubsidizedRoute>();
}
