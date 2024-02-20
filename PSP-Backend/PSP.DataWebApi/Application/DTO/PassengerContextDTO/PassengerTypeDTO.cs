namespace Application.DTO.PassengerContextDTO;

public class PassengerTypeDTO
{
    public string Code { get; set; }
    
    public string Type { get; set; }
    
    public string Description { get; set; }
    
    public List<short> Appendices { get; set; }
    
    public List<string> QuotaCategories { get; set; }
}