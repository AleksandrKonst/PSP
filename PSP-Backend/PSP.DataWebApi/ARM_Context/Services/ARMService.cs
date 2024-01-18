using System.Dynamic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PSP.DataWebApi.ARM_Context.DTO;
using PSP.DataWebApi.ARM_Context.Services.Interfaces;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataWebApi.ARM_Context.Services;

public class ARMService(IPassengerQuotaCountRepository passengerQuotaCountRepository, IPassengerRepository passengerRepository, IMapper mapper) : IARMService
{
    public async Task<List<dynamic>> SelectAsync(IEnumerable<SelectPassengerRequestDTO> selectPassengerRequestsDto)
    {
        var passengers = new List<dynamic>();
        
        foreach (var selectPassengerRequestDto in selectPassengerRequestsDto)
        {
            dynamic passenger = new ExpandoObject();
            passenger.id = selectPassengerRequestDto.Id;
            
            var passengerFromDb = await passengerRepository.GetAll().Where(p => p.Name == selectPassengerRequestDto.Name && 
                    p.Surname == selectPassengerRequestDto.Surname && 
                    p.Patronymic == selectPassengerRequestDto.Patronymic && 
                    p.Birthdate == selectPassengerRequestDto.Birthdate)
                .FirstOrDefaultAsync();
            
            if (passengerFromDb != null)
            {
                passenger.passenger_data = new SelectPassengerResponseDTO()
                {
                    Birthdate = passengerFromDb.Birthdate,
                    Gender = passengerFromDb.Gender,
                    DocumentType = passengerFromDb.DocumentTypeCode,
                    DocumentNumber = passengerFromDb.DocumentNumber,
                    DocumentNumbersLatin = passengerFromDb.DocumentNumbersLatin
                };
                
                //проверка валидности пользователя во внешнем сервисе
                
                passenger.identity_confirmation = new
                {
                    Confirmed = true,
                    Code = "PIC-000000",
                    Message = "Успешное подтверждение личности гражданина"
                };

                var typeConfirmations = new List<dynamic>();

                if (selectPassengerRequestDto.Types != null)
                {
                    foreach (var type in selectPassengerRequestDto.Types)
                    {
                        dynamic typeConfirmation = new ExpandoObject();
                        typeConfirmation.type = type;
                        
                        if (passengerFromDb.PassengerTypes.Contains(type))
                        {
                            typeConfirmation.status = "confirmed";
                            typeConfirmation.code = "PTC-000000";
                            typeConfirmation.message = "Успешное подтверждение типа пассажира";
                        }
                        typeConfirmation.status = "not confirmed";
                        typeConfirmation.code = "PTC-000001";
                        
                        typeConfirmations.Add(typeConfirmation);
                    } 
                }
                
                passenger.type_confirmations = typeConfirmations;
                
                var quotaYearBalances = new List<dynamic>();

                if (selectPassengerRequestDto.QuotaBalancesYears != null)
                {
                    foreach (var quotaYear in selectPassengerRequestDto.QuotaBalancesYears)
                    {
                        dynamic quotaBalance = new ExpandoObject();
                        
                        quotaBalance.year = quotaYear;
                        quotaBalance.used_documents_count = 1;
                        
                        var quotaBalances = new List<QuotBalanceDTO>();

                        var quotaCounts = await passengerQuotaCountRepository.GetAll().Where(qc =>
                            qc.PassengerId == selectPassengerRequestDto.Id && qc.QuotaYear == quotaYear.ToString()).ToListAsync();

                        foreach (var quotaCount in quotaCounts)
                        {
                            quotaBalances.Add(new QuotBalanceDTO
                            {
                                Category = quotaCount.QuotaCategoriesCode,
                                Available = quotaCount.AvailableCount,
                                Issued = quotaCount.IssuedCount,
                                Refund = quotaCount.RefundCount,
                                Used = quotaCount.UsedCount
                            });
                        }
                        quotaBalance.category_balances = quotaBalances;
                        
                        quotaYearBalances.Add(quotaBalance);
                    } 
                }
                
                passenger.quota_balances = quotaYearBalances;
            }
            else
            {
                passenger.passenger_data = new SelectPassengerResponseDTO()
                {
                    Birthdate = selectPassengerRequestDto.Birthdate,
                    Gender = selectPassengerRequestDto.Gender,
                    DocumentType = selectPassengerRequestDto.DocumentType,
                    DocumentNumber = selectPassengerRequestDto.DocumentNumber,
                    DocumentNumbersLatin = new List<string>() {selectPassengerRequestDto.DocumentNumber} //транслировать
                };
            }
            
            passengers.Add(passenger);
        }

        return passengers;
    }
}