namespace PSP.DataApplication.DTO;

public class InsertPassengerRequestDTO
{
    public string OperationType { get; set; }
    
    public string OperationDatetime { get; set; }
    
    public string OperationPlace { get; set; }
    
    public List<InsertPassengerDataDTO> Passengers { get; set; }
    
    public List<InsertCouponDTO> Coupons { get; set; }
}

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

public class InsertCouponDTO
{
    public string AirlineCode { get; set; }
    
    public int FlightNumber { get; set; }
    
    public string OperationPlace { get; set; }
    
    public string DepartPlace { get; set; }
    
    public string DepartDateTimePlan { get; set; }
    
    public string ArrivePlace { get; set; }
    
    public string ArriveTimePlan { get; set; }
    
    public string PnrCode { get; set; }
    
    public List<InsertFaresDTO> Fares { get; set; }
}

public class InsertFaresDTO
{
    public int PassengerId { get; set; }
    
    public string PassengerType { get; set; }
    
    public string Code { get; set; }

    public decimal Amount { get; set; }
    
    public string Currency { get; set; }
    
    public bool Special { get; set; }
}