using Microsoft.EntityFrameworkCore;
using PSP_Data_Service.Passenger_Context.Infrastructure.Exceptions;
using PSP_Data_Service.Passenger_Context.Models;
using PSP_Data_Service.Passenger_Context.Repositories.Interfaces;

namespace PSP_Data_Service.Passenger_Context.Repositories;

public class DocumentTypeRepository(PassengerDataContext context) : IDocumentTypeRepository
{
    public IQueryable<DocumentType> GetAll() => context.DictDocumentTypes;

    public async Task<DocumentType> GetByIdAsync(string code)
    {
        var documentType = await context.DictDocumentTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (documentType == null) throw new ResponseException("Тип документа не найден", "PPC-000001");
        return documentType;
    }
    
    public async Task<bool> Update(DocumentType documentType)
    {
        var dbPassenger = await context.DictDocumentTypes.AnyAsync(p => p.Code == documentType.Code);
        if (!dbPassenger) throw new ResponseException("Тип документа не найден", "PPC-000001");
        
        var updatePassengerType = context.Update(documentType);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Add(DocumentType documentType)
    {
        var dbPassenger = await context.DictDocumentTypes.AnyAsync(p => p.Code == documentType.Code);
        if (dbPassenger) throw new ResponseException("Тип документа уже существует", "PPC-000002");

        var newPassengerType = await context.DictDocumentTypes.AddAsync(documentType);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(string code)
    {
        var dbPassengerType = await context.DictDocumentTypes.Where(p => p.Code == code).FirstOrDefaultAsync();
        if (dbPassengerType == null) throw new ResponseException("Тип документа не найден", "PPC-000001");
        
        context.DictDocumentTypes.Remove(dbPassengerType);
        await context.SaveChangesAsync();
        return true;
    }
}