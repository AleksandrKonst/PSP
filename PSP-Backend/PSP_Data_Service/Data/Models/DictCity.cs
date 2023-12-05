namespace PSP_Data_Service.Data.Models;

/// <summary>
/// Населенные пункты
/// </summary>
public partial class DictCity
{
    /// <summary>
    /// Код IATA населенного пункта
    /// </summary>
    public string IataCode { get; set; } = null!;

    /// <summary>
    /// Название населенного пункта
    /// </summary>
    public string Name { get; set; } = null!;

    public virtual ICollection<DictAirport> DictAirports { get; set; } = new List<DictAirport>();

    public virtual ICollection<DictSubsidizedRoute> DictSubsidizedRouteCityFinishIataCodeNavigations { get; set; } = new List<DictSubsidizedRoute>();

    public virtual ICollection<DictSubsidizedRoute> DictSubsidizedRouteCityStartIataCodeNavigations { get; set; } = new List<DictSubsidizedRoute>();
}
