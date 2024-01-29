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
    /// Тип документа
    /// </summary>
    public string DocumentTypeCode { get; set; } = null!;

    /// <summary>
    /// Номер документа
    /// </summary>
    public string DocumentNumber { get; set; } = null!;

    /// <summary>
    /// Номер документа latin
    /// </summary>
    public string DocumentNumberLatin { get; set; } = null!;

    /// <summary>
    /// Код квотирования
    /// </summary>
    public string QuotaCode { get; set; } = null!;

    /// <summary>
    /// Код перелета
    /// </summary>
    public int FlightCode { get; set; }

    /// <summary>
    /// Тип билета
    /// </summary>
    public short TicketType { get; set; }

    /// <summary>
    /// Номер билета
    /// </summary>
    public string TicketNumber { get; set; } = null!;

    public virtual DocumentType DocumentTypeCodeNavigation { get; set; } = null!;

    public virtual Flight FlightCodeNavigation { get; set; } = null!;

    public virtual OperationType OperationTypeNavigation { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual QuotaCategory QuotaCodeNavigation { get; set; } = null!;

    public virtual TicketType TicketTypeNavigation { get; set; } = null!;
}
