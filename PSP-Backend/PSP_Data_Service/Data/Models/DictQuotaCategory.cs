namespace PSP_Data_Service.Data.Models;

/// <summary>
/// Категории квотирования
/// </summary>
public partial class DictQuotaCategory
{
    /// <summary>
    /// Код категории квотирования
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Категория квотирования
    /// </summary>
    public string Category { get; set; } = null!;

    /// <summary>
    /// Список номеров приложения к Постановлению Правительства РФ №215
    /// </summary>
    public List<short> Appendices { get; set; } = null!;

    /// <summary>
    /// Квота в одном направлении
    /// </summary>
    public short OneWayQuota { get; set; }

    /// <summary>
    /// Квота в направлении туда и обратно
    /// </summary>
    public short RoundTripQuota { get; set; }

    public virtual ICollection<ConPassengerQuotaCount> ConPassengerQuotaCounts { get; set; } = new List<ConPassengerQuotaCount>();

    public virtual ICollection<DataCouponEvent> DataCouponEvents { get; set; } = new List<DataCouponEvent>();
}
