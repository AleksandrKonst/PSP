namespace PSP.DataApplication.DTO.ArmContextDTO.Select;

public class SelectPassengerDataDTO
{
    public DateOnly Birthdate { get; set; }

    public string Gender { get; set; }
    
    public string DocumentType { get; set; }
    
    public string DocumentNumber { get; set; }
    
    public List<string>? DocumentNumbersLatin { get; set; }
}