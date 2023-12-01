using System;
using System.Collections.Generic;

namespace PSP_Data_Service.Models;

public partial class DictFlight
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

    public virtual DictAirline AirlineCodeNavigation { get; set; } = null!;

    public virtual DictAirport ArrivePlaceNavigation { get; set; } = null!;

    public virtual ICollection<ConFlightPart> ConFlightParts { get; set; } = new List<ConFlightPart>();

    public virtual ICollection<DataCouponEvent> DataCouponEvents { get; set; } = new List<DataCouponEvent>();

    public virtual DictAirport DepartPlaceNavigation { get; set; } = null!;
}
