namespace PSP_Data_Service.Passenger_Context.Models;

/// <summary>
/// Полы
/// </summary>
public class GenderType
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
