namespace PSP.DataApplication.DTO;

public class SelectPassengerResponseDTO
{
    public DateOnly Birthdate { get; set; }

    public string Gender { get; set; }
    
    public string DocumentType { get; set; }
    
    public string DocumentNumber { get; set; }
    
    public List<string>? DocumentNumbersLatin { get; set; }
}