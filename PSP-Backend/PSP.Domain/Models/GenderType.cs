namespace PSP.Domain.Models;

/// <summary>
/// Полы
/// </summary>
public partial class GenderType
{
    /// <summary>
    /// Код пола
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Пол
    /// </summary>
    public string Gender { get; set; } = null!;

    public virtual ICollection<Passenger> DataPassengers { get; set; } = new List<Passenger>();
}
