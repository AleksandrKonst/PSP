namespace PSP.Domain.Models;

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

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
