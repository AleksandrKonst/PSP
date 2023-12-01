using System;
using System.Collections.Generic;

namespace PSP_Data_Service.Models;

/// <summary>
/// Типы перевозочных документов
/// </summary>
public partial class DictTicketType
{
    /// <summary>
    /// Код типа перевозочного документа
    /// </summary>
    public short Code { get; set; }

    /// <summary>
    /// Тип перевозочного документа
    /// </summary>
    public string Type { get; set; } = null!;

    public virtual ICollection<DataCouponEvent> DataCouponEvents { get; set; } = new List<DataCouponEvent>();
}
