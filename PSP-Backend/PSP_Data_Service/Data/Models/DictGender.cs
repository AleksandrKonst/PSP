namespace PSP_Data_Service.Data.Models;

/// <summary>
/// Полы
/// </summary>
public partial class DictGender
{
    /// <summary>
    /// Код пола
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Пол
    /// </summary>
    public string Gender { get; set; } = null!;

    public virtual ICollection<DataPassenger> DataPassengers { get; set; } = new List<DataPassenger>();
}
