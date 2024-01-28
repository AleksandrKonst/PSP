namespace PSP.Domain.Models;

public class Passenger
{
    /// <summary>
    /// Идентификатор пассажира
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Имя пассажира
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Фамилия пассажира
    /// </summary>
    public string Surname { get; set; } = null!;

    /// <summary>
    /// Отчество пассажира
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly Birthdate { get; set; }

    /// <summary>
    /// Пол пассажира
    /// </summary>
    public string Gender { get; set; } = null!;

    /// <summary>
    /// Типы пассажира
    /// </summary>
    public List<string> PassengerTypes { get; set; }

    public virtual ICollection<CouponEvent> CouponEvents { get; set; } = new List<CouponEvent>();

    public virtual GenderType GenderNavigation { get; set; } = null!;
}
