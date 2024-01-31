namespace Domain.Models;

public class OperationType
{
    /// <summary>
    /// Код операции
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Описание операции
    /// </summary>
    public string? OperationDescription { get; set; }
    
    public virtual ICollection<CouponEvent> CouponEvents { get; set; } = new List<CouponEvent>();
}