namespace PSP.DataWebApi.Contexts.Passenger_Context.DTO;

public class PostPassengerDTO
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string? Patronymic { get; set; }
    
    public DateOnly Birthdate { get; set; }
    
    public string Gender { get; set; }
    
    public string DocumentTypeCode { get; set; }
    
    public string DocumentNumber { get; set; }
    
    public List<string>? DocumentNumbersLatin { get; set; }
    
    public List<string>? PassengerTypes { get; set; }
}