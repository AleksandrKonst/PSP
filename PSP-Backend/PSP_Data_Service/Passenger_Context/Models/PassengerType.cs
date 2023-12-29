namespace PSP_Data_Service.Passenger_Context.Models;

/// <summary>
/// Типы пассажиров
/// </summary>
public partial class PassengerType
{
    /// <summary>
    /// Код типа пассажира
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Тип пассажира
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Описание типа пассажира
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Список номеров приложений к Постановлению Правительства РФ №215
    /// </summary>
    public List<short> Appendices { get; set; } = null!;

    /// <summary>
    /// Список ссылок на категории квотирования
    /// </summary>
    public List<string>? QuotaCategories { get; set; }
}
