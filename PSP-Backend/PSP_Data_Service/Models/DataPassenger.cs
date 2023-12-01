﻿using System;
using System.Collections.Generic;

namespace PSP_Data_Service.Models;

public partial class DataPassenger
{
    /// <summary>
    /// Идентификатор пассажира
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Имя пассажира
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Фамилия пассажира
    /// </summary>
    public string Surname { get; set; } = null!;

    /// <summary>
    /// Отчество пассажира
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly Birthdate { get; set; }

    /// <summary>
    /// Пол пассажира
    /// </summary>
    public string Gender { get; set; } = null!;

    /// <summary>
    /// Тип документа
    /// </summary>
    public string DocumentTypeCode { get; set; } = null!;

    /// <summary>
    /// Серия документа
    /// </summary>
    public string DocumentNumber { get; set; } = null!;

    /// <summary>
    /// Разлиные варианты названий документов
    /// </summary>
    public List<string>? DocumentNumbersLatin { get; set; }

    /// <summary>
    /// Типы пассажира
    /// </summary>
    public List<string>? PassengerTypes { get; set; }

    public virtual ICollection<ConPassengerQuotaCount> ConPassengerQuotaCounts { get; set; } = new List<ConPassengerQuotaCount>();

    public virtual ICollection<DataCouponEvent> DataCouponEvents { get; set; } = new List<DataCouponEvent>();

    public virtual DictDocumentType DocumentTypeCodeNavigation { get; set; } = null!;

    public virtual DictGender GenderNavigation { get; set; } = null!;
}