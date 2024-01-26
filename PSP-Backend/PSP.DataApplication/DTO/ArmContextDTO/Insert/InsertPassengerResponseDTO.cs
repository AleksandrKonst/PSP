namespace PSP.DataApplication.DTO.ArmContextDTO.Insert;

public class InsertPassengerResponseDTO
{
    public int Id { get; set; }
    
    public TicketPropertiesDTO TicketProperties { get; set; }
    
    public List<QuotaBalanceDTO> QuotaBalances { get; set; }
}