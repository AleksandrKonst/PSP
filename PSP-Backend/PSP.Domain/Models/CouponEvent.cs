namespace PSP.Domain.Models;

/// <summary>
/// События с купонами
/// </summary>
public class CouponEvent
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
    /// Тип билета
    /// </summary>
    public short TicketType { get; set; }

    /// <summary>
    /// Код маршрута
    /// </summary>
    public int FlightCode { get; set; }

    
    /// <summary>
    /// Код тарифа
    /// </summary>
    public string FareCode { get; set; } = null!;
    
    public virtual Fare Fare { get; set; } = null!;
    
    public virtual Flight Flight { get; set; } = null!;
    
    public virtual Passenger Passenger { get; set; } = null!;

    public virtual TicketType TicketTypeNavigation { get; set; } = null!;
}
