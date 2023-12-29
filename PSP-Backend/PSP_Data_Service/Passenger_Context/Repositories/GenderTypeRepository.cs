using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Repositories;

public class GenderTypeRepository : IGenderTypeRepository
{
    private readonly PassengerDataContext _context;

    public GenderTypeRepository(PassengerDataContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<GenderType>> GetAll()
    {
        return await _context.DictGenders.ToListAsync();
    }
}