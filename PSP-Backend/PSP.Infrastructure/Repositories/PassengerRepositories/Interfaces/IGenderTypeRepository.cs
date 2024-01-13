using PSP.Domain.Models;

namespace PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

public interface IGenderTypeRepository
{
    IQueryable<GenderType> GetAll();
    Task<GenderType> GetByIdAsync(string code);
}