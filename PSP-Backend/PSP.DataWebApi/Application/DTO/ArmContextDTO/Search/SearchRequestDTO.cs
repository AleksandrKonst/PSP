namespace Application.DTO.ArmContextDTO.Search;

public class SearchRequestDTO
{
    public string SearchType { get; set; }
    
    public int? QuotaBalancesYear { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }
    
    public DateOnly? Birthdate { get; set; }

    public string? Gender { get; set; }
    
    public string? DocumentType { get; set; }
    
    public string? DocumentNumber { get; set; }

    public string? Snils { get; set; }
    
    public int? TicketType { get; set; }

    public string? TicketNumber { get; set; }
}