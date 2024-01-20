namespace PSP.Domain.Models;

public class OperationType
{
    /// <summary>
    /// Код операции
    /// </summary>
    public string OperationCode { get; set; } = null!;

    /// <summary>
    /// Описание операции
    /// </summary>
    public string? OperationDescription { get; set; }

    public virtual ICollection<CouponEvent> DataCouponEvents { get; set; } = new List<CouponEvent>();
}