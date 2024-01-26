namespace PSP.DataApplication.DTO.ArmContextDTO.Insert;

public class InsertPassengerDataDTO
{
    public int Id { get; set; }

    public string Surname { get; set; }

    public string Name { get; set; }

    public string? Patronymic { get; set; }
    
    public DateOnly Birthdate { get; set; }

    public string Gender { get; set; }
    
    public string DocumentType { get; set; }
    
    public string DocumentNumber { get; set; }

    public short TicketType { get; set; }
    
    public string TicketNumber { get; set; }

    public List<int> QuotaBalancesYears { get; set; }
}