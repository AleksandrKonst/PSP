namespace Domain.Models;

/// <summary>
/// Субсидированные направления
/// </summary>
public class SubsidizedRoute
{
    /// <summary>
    /// Идентификатор субсидированного направления
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Ссылка на начальный населенный пункт направления
    /// </summary>
    public string CityStartIataCode { get; set; } = null!;

    /// <summary>
    /// Ссылка на конечный населенный пункт направления
    /// </summary>
    public string CityFinishIataCode { get; set; } = null!;

    /// <summary>
    /// Номер приложения к Постановлению Правительства РФ №215
    /// </summary>
    public short Appendix { get; set; }

    /// <summary>
    /// Размер специального тарифа для направления
    /// </summary>
    public int FareAmount { get; set; }

    /// <summary>
    /// Размер субсидии для направления
    /// </summary>
    public int SubsidyAmount { get; set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; set; } = null!;

    /// <summary>
    /// Начало периода актуальности записи
    /// </summary>
    public DateTime ValidityFrom { get; set; }

    /// <summary>
    /// Окончание периода актуальности записи
    /// </summary>
    public DateTime ValidityTo { get; set; }

    /// <summary>
    /// Список промежуточных населенных пунктов направления
    /// </summary>
    public List<string>? InteriorCities { get; set; }

    public virtual City CityFinishIataCodeNavigation { get; set; } = null!;

    public virtual City CityStartIataCodeNavigation { get; set; } = null!;
}
