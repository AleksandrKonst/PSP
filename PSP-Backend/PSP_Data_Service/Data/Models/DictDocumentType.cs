namespace PSP_Data_Service.Data.Models;

/// <summary>
/// Типы документов, удостоверяющих личность
/// </summary>
public partial class DictDocumentType
{
    /// <summary>
    /// Код типа документа, удостоверяющего личность
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Тип документа, удостоверяющего личность
    /// </summary>
    public string Type { get; set; } = null!;

    public virtual ICollection<DataPassenger> DataPassengers { get; set; } = new List<DataPassenger>();
}
