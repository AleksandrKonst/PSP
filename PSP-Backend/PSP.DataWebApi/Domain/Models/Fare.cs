namespace Domain.Models;

public class Fare
{
    /// <summary>
    /// Идендификатор тарифа
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Стоимость тарифа
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; set; } = null!;

    /// <summary>
    /// Специальный тариф
    /// </summary>
    public bool Special { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}