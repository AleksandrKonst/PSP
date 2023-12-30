using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Repositories;

public class PassengerRepository : IPassengerRepository
{
    private readonly PassengerDataContext _context;

    public PassengerRepository(PassengerDataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Passenger>> GetAllAsync()
    {
        return await _context.DataPassengers.ToListAsync();
    }
}