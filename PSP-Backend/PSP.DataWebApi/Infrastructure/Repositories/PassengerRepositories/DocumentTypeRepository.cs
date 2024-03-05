using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.PassengerRepositories;

public class DocumentTypeRepository(PSPContext context) : IDocumentTypeRepository
{
    public async Task<IEnumerable<DocumentType>> GetAllAsync() => await context.DocumentTypes.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<DocumentType>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.DocumentTypes.Skip(index).Take(count).AsNoTracking().ToListAsync();

    public async Task<DocumentType?> GetByCodeAsync(string code) => await context.DocumentTypes.Where(p => p.Code == code).AsNoTracking().FirstOrDefaultAsync();

    public async Task<long> GetCountAsync() => await context.DocumentTypes.AsNoTracking().CountAsync();

    public async Task<bool> CheckByCodeAsync(string code) => await context.DocumentTypes.Where(p => p.Code == code).AsNoTracking().AnyAsync();
    
    public async Task<bool> AddAsync(DocumentType obj)
    {
        await context.DocumentTypes.AddAsync(obj);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(DocumentType obj)
    {
        context.Update(obj);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var dbPassengerType = await context.DocumentTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbPassengerType == null) return false;
        
        context.DocumentTypes.Remove(dbPassengerType);
        await context.SaveChangesAsync();
        return true;
    }
}