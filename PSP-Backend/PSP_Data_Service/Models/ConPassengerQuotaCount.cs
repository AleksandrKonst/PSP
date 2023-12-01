using System;
using System.Collections.Generic;

namespace PSP_Data_Service.Models;

public partial class ConPassengerQuotaCount
{
    /// <summary>
    /// Идендификатор пассажира
    /// </summary>
    public long PassengerId { get; set; }

    /// <summary>
    /// Код категории квотирования
    /// </summary>
    public string QuotaCategoriesCode { get; set; } = null!;

    /// <summary>
    /// Год квотирования
    /// </summary>
    public string QuotaYear { get; set; } = null!;

    /// <summary>
    /// Колличество оформленных билетов
    /// </summary>
    public short IssuedCount { get; set; }

    /// <summary>
    /// Колличество возращенных билетов
    /// </summary>
    public short RefundCount { get; set; }

    /// <summary>
    /// Колличество использованых билетов
    /// </summary>
    public short UsedCount { get; set; }

    /// <summary>
    /// Колличество доступных квот
    /// </summary>
    public short AvailableCount { get; set; }

    public virtual DataPassenger Passenger { get; set; } = null!;

    public virtual DictQuotaCategory QuotaCategoriesCodeNavigation { get; set; } = null!;
}
