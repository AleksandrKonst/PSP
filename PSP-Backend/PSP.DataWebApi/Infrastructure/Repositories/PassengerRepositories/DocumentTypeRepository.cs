using Domain.Models;
using Infrastructure.Data.Context;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.PassengerRepositories;

public class DocumentTypeRepository(PSPContext context) : IDocumentTypeRepository
{
    public async Task<List<DocumentType>> GetAllAsync() => await context.DocumentTypes.ToListAsync();

    public async Task<List<DocumentType>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => await context.DocumentTypes.Skip(index).Take(count).ToListAsync();

    public async Task<DocumentType?> GetByCodeAsync(string code) => await context.DocumentTypes.Where(p => p.Code == code).FirstOrDefaultAsync();

    public async Task<int> GetCountAsync() => await context.DocumentTypes.CountAsync();

    public async Task<bool> CheckByCodeAsync(string code) => await context.DocumentTypes.Where(p => p.Code == code).AnyAsync();
    
    public async Task<bool> AddAsync(DocumentType documentType)
    {
        var newPassengerType = await context.DocumentTypes.AddAsync(documentType);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> UpdateAsync(DocumentType documentType)
    {
        var updatePassengerType = context.Update(documentType);
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