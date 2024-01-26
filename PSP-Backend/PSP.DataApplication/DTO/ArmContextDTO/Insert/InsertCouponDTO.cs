namespace PSP.DataApplication.DTO.ArmContextDTO.Insert;

public class InsertCouponDTO
{
    public string AirlineCode { get; set; }
    
    public int FlightNumber { get; set; }
    
    public string OperationPlace { get; set; }
    
    public string DepartDateTimePlan { get; set; }
    
    public string ArrivePlace { get; set; }
    
    public string ArriveTimePlan { get; set; }
    
    public List<InsertFaresDTO> Fares { get; set; }
}