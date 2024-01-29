namespace PSP.DataApplication.DTO.FlightContextDTO;

public class FlightDTO
{
    public int Code { get; set; }
    
    public string AirlineCode { get; set; }
    
    public string DepartPlace { get; set; }
    
    public DateTime DepartDateTimePlan { get; set; }
    
    public string ArrivePlace { get; set; }
    
    public DateTime ArriveDateTimePlan { get; set; }
    
    public string PnrCode { get; set; }
    
    public string FareCode { get; set; }
}