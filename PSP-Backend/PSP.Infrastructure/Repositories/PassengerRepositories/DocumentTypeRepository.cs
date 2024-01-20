using Microsoft.EntityFrameworkCore;
using PSP.Domain.Exceptions;
using PSP.Domain.Models;
using PSP.Infrastructure.Data.Context;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.Infrastructure.Repositories.PassengerRepositories;

public class DocumentTypeRepository(PSPContext context) : IDocumentTypeRepository
{
    public Task<List<DocumentType>> GetAllAsync() => context.DictDocumentTypes.ToListAsync();

    public Task<List<DocumentType>> GetPartAsync(int index = 0, int count = Int32.MaxValue) => context.DictDocumentTypes.Skip(index).Take(count).ToListAsync();

    public async Task<DocumentType> GetByIdAsync(string code)
    {
        var documentType = await context.DictDocumentTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (documentType == null) throw new ResponseException("Тип документа не найден", "PPC-000001");
        return documentType;
    }

    public Task<int> GetCountAsync() => context.DictDocumentTypes.CountAsync();

    public async Task<bool> UpdateAsync(DocumentType documentType)
    {
        var dbPassenger = await context.DictDocumentTypes.AnyAsync(p => p.Code == documentType.Code);
        if (!dbPassenger) throw new ResponseException("Тип документа не найден", "PPC-000001");
        
        var updatePassengerType = context.Update(documentType);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddAsync(DocumentType documentType)
    {
        var dbPassenger = await context.DictDocumentTypes.AnyAsync(p => p.Code == documentType.Code);
        if (dbPassenger) throw new ResponseException("Тип документа уже существует", "PPC-000002");

        var newPassengerType = await context.DictDocumentTypes.AddAsync(documentType);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(string code)
    {
        var dbPassengerType = await context.DictDocumentTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbPassengerType == null) throw new ResponseException("Тип документа не найден", "PPC-000001");
        
        context.DictDocumentTypes.Remove(dbPassengerType);
        await context.SaveChangesAsync();
        return true;
    }
}