namespace PSP_Data_Service.Data.Models;

/// <summary>
/// События с купонами
/// </summary>
public partial class DataCouponEvent
{
    /// <summary>
    /// Идентификатор события с купоном
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Тип операции с билетами
    /// </summary>
    public string OperationType { get; set; } = null!;

    /// <summary>
    /// Дата и время операции с билетами (UTC)
    /// </summary>
    public DateTime OperationDatetimeUtc { get; set; }

    /// <summary>
    /// Временная зона времени операции с билетами
    /// </summary>
    public short OperationDatetimeTimezone { get; set; }

    /// <summary>
    /// Место проведения операции с билетами
    /// </summary>
    public string? OperationPlace { get; set; }

    /// <summary>
    /// Идентификатор пассажира
    /// </summary>
    public long PassengerId { get; set; }

    /// <summary>
    /// Идентификатор субсидируемого маршрута
    /// </summary>
    public int SubsidizedRouteId { get; set; }

    /// <summary>
    /// Тип билета
    /// </summary>
    public short TicketType { get; set; }

    /// <summary>
    /// Код категории квотирования
    /// </summary>
    public string QuotaCategoryCode { get; set; } = null!;

    /// <summary>
    /// Код маршрута
    /// </summary>
    public int FlightCode { get; set; }

    public virtual DictFlight FlightCodeNavigation { get; set; } = null!;

    public virtual DataPassenger Passenger { get; set; } = null!;

    public virtual DictQuotaCategory QuotaCategoryCodeNavigation { get; set; } = null!;

    public virtual DictSubsidizedRoute SubsidizedRoute { get; set; } = null!;

    public virtual DictTicketType TicketTypeNavigation { get; set; } = null!;
}
