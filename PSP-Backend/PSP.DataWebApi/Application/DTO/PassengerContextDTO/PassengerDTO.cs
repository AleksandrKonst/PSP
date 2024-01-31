namespace Application.DTO.PassengerContextDTO;

public class PassengerDTO
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string? Patronymic { get; set; }
    
    public DateOnly Birthdate { get; set; }
    
    public string Gender { get; set; }
    
    public List<string>? PassengerTypes { get; set; }
}