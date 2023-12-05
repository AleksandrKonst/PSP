namespace PSP_Data_Service.Data.Models;

public partial class ConFlightPart
{
    /// <summary>
    /// Код маршрута
    /// </summary>
    public int FlightCode { get; set; }

    /// <summary>
    /// Номер части маршрута
    /// </summary>
    public short FlightPart { get; set; }

    /// <summary>
    /// Аэрпорт отлета
    /// </summary>
    public string DepartPlace { get; set; } = null!;

    /// <summary>
    /// Аэропорт прилета
    /// </summary>
    public string ArrivePlace { get; set; } = null!;

    /// <summary>
    /// Время отлета
    /// </summary>
    public DateTime FlightDepartDatetime { get; set; }

    /// <summary>
    /// Время прилета
    /// </summary>
    public DateTime FlightArriveDatetime { get; set; }

    public virtual DictAirport ArrivePlaceNavigation { get; set; } = null!;

    public virtual DictAirport DepartPlaceNavigation { get; set; } = null!;

    public virtual DictFlight FlightCodeNavigation { get; set; } = null!;
}
