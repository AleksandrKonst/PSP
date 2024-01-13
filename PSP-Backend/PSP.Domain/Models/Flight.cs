namespace PSP.Domain.Models;
public partial class Flight
{
    /// <summary>
    /// Код маршрута
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Код авиаперевочика
    /// </summary>
    public string AirlineCode { get; set; } = null!;

    /// <summary>
    /// Код аэропорта отправки
    /// </summary>
    public string DepartPlace { get; set; } = null!;

    /// <summary>
    /// Код аэропорта прилета
    /// </summary>
    public string ArrivePlace { get; set; } = null!;

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

    public virtual Airport ArrivePlaceNavigation { get; set; } = null!;

    public virtual ICollection<PartOfFlight> ConFlightParts { get; set; } = new List<PartOfFlight>();

    public virtual ICollection<CouponEvent> DataCouponEvents { get; set; } = new List<CouponEvent>();

    public virtual Airport DepartPlaceNavigation { get; set; } = null!;
}
