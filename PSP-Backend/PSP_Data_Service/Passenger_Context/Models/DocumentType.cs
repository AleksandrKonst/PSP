namespace PSP_Data_Service.Passenger_Context.Models;

/// <summary>
/// Типы документов, удостоверяющих личность
/// </summary>
public partial class DocumentType
{
    /// <summary>
    /// Код типа документа, удостоверяющего личность
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Тип документа, удостоверяющего личность
    /// </summary>
    public string Type { get; set; } = null!;

    public virtual ICollection<Passenger> DataPassengers { get; set; } = new List<Passenger>();
}
