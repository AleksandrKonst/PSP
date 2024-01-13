namespace PSP.Domain.Models;

/// <summary>
/// Типы перевозочных документов
/// </summary>
public class TicketType
{
    /// <summary>
    /// Код типа перевозочного документа
    /// </summary>
    public short Code { get; set; }

    /// <summary>
    /// Тип перевозочного документа
    /// </summary>
    public string Type { get; set; } = null!;

    public virtual ICollection<CouponEvent> DataCouponEvents { get; set; } = new List<CouponEvent>();
}
