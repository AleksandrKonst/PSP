namespace Domain.Models;

/// <summary>
/// Типы документов, удостоверяющих личность
/// </summary>
public class DocumentType
{
    /// <summary>
    /// Код типа документа, удостоверяющего личность
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Тип документа, удостоверяющего личность
    /// </summary>
    public string Type { get; set; } = null!;

    public virtual ICollection<CouponEvent> CouponEvents { get; set; } = new List<CouponEvent>();
}