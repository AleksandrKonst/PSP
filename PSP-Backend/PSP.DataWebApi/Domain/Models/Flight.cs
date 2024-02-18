namespace Domain.Models;
public class Flight
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
    /// Время отправки
    /// </summary>
    public DateTime DepartDatetimePlan { get; set; }

    /// <summary>
    /// Код аэропорта посадки
    /// </summary>
    public string ArrivePlace { get; set; } = null!;

    /// <summary>
    /// Время посадки
    /// </summary>
    public DateTime ArriveDatetimePlan { get; set; }

    /// <summary>
    /// PNR код
    /// </summary>
    public string PnrCode { get; set; } = null!;

    /// <summary>
    /// Код тарифа
    /// </summary>
    public string FareCode { get; set; } = null!;

    public virtual Airline AirlineCodeNavigation { get; set; } = null!;
    
    public virtual Airport ArrivePlaceNavigation { get; set; } = null!;

    public virtual ICollection<CouponEvent> CouponEvents { get; set; } = new List<CouponEvent>();

    public virtual Airport DepartPlaceNavigation { get; set; } = null!;

    public virtual Fare FareCodeNavigation { get; set; } = null!;
}
