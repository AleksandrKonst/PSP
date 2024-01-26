namespace PSP.Domain.Models;

public class Fare
{
    /// <summary>
    /// Идендификатор тарифа
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Название тарифа
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Код категории квотирования
    /// </summary>
    public string QuotaCategoryCode { get; set; } = null!;

    /// <summary>
    /// Идентификатор направления
    /// </summary>
    public int? SubsidizedRouteId { get; set; }

    /// <summary>
    /// Код авиакомпании
    /// </summary>
    public string AirlineCode { get; set; } = null!;

    /// <summary>
    /// Стоимость льготного билета
    /// </summary>
    public decimal SubsitizedAmount { get; set; }

    /// <summary>
    /// Колличество субсидий
    /// </summary>
    public decimal PreferentialAmount { get; set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; set; } = null!;

    public virtual Airline AirlineCodeNavigation { get; set; } = null!;

    public virtual ICollection<CouponEvent> DataCouponEvents { get; set; } = new List<CouponEvent>();

    public virtual QuotaCategory QutoaCategoryCodeNavigation { get; set; } = null!;

    public virtual SubsidizedRoute? SubsidizedRoute { get; set; }
}