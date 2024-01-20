namespace PSP.DataApplication.DTO;

public class SelectPassengerRequestDTO
{
    public long Id { get; set; }

    public string Surname { get; set; }

    public string Name { get; set; }

    public string? Patronymic { get; set; }
    
    public DateOnly Birthdate { get; set; }

    public string Gender { get; set; }
    
    public string DocumentType { get; set; }
    
    public string DocumentNumber { get; set; }

    public string Snils { get; set; }

    public List<int>? QuotaBalancesYears { get; set; }
    
    public List<string>? Types { get; set; }

    public List<string>? AdpPassengerIds { get; set; }
}