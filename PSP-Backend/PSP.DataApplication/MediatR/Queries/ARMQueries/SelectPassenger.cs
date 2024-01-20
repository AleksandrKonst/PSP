using System.Dynamic;
using AutoMapper;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Domain.Exceptions;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.ARMQueries;

public class SelectPassenger
{
    public record Query(IEnumerable<SelectPassengerRequestDTO> SelectPassengerRequest) : IRequest<QueryResult>;
    
    public record QueryResult(List<dynamic> Result);
    
    public class Handler(IPassengerRepository passengerRepository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var passengers = new List<dynamic>();
        
            foreach (var requestDto in request.SelectPassengerRequest)
            {
                dynamic passenger = new ExpandoObject();
                passenger.id = requestDto.Id;

                Passenger? passengerFromDb = null;

                if (await passengerRepository.CheckByFullNameAsync(requestDto.Name, requestDto.Surname, requestDto.Patronymic, requestDto.Birthdate))
                {
                    passengerFromDb = await passengerRepository
                        .GetByIdWithCouponEventAsync(requestDto.Name, requestDto.Surname, requestDto.Patronymic, requestDto.Birthdate, requestDto.QuotaBalancesYears);
                    
                    if (passengerFromDb == null) throw new ResponseException("Пассажир не найден", "PPC-000001");
                }
                else
                {
                    //Add and Count
                }
                
                passenger.passenger_data = mapper.Map<SelectPassengerResponseDTO>(passengerFromDb);
                
                passenger.identity_confirmation = new
                {
                    Confirmed = true,
                    Code = "PIC-000000",
                    Message = "Успешное подтверждение личности гражданина"
                };

                var typeConfirmations = new List<dynamic>();
                if (requestDto.Types != null)
                {
                    foreach (var type in requestDto.Types)
                    {
                        dynamic typeConfirmation = new ExpandoObject();
                        typeConfirmation.type = type;
                        
                        if (passengerFromDb.PassengerTypes.Contains(type))
                        {
                            typeConfirmation.status = "confirmed";
                            typeConfirmation.code = "PTC-000000";
                            typeConfirmation.message = "Успешное подтверждение типа пассажира";
                        }
                        else
                        {
                            typeConfirmation.status = "not confirmed";
                            typeConfirmation.code = "PTC-000001";
                            typeConfirmation.message = "Тип пассажира не подтвержден";
                        }
                        
                        typeConfirmations.Add(typeConfirmation);
                    } 
                }
                passenger.type_confirmations = typeConfirmations;
                
                var quotaYearBalances = new List<dynamic>();
                if (requestDto.QuotaBalancesYears != null)
                {
                    foreach (var quotaYear in requestDto.QuotaBalancesYears)
                    {
                        dynamic quotaBalance = new ExpandoObject();
                        
                        quotaBalance.year = quotaYear;
                        quotaBalance.used_documents_count = 1;
                        
                        var quotaBalances = new List<QuotBalanceDTO>();
                    
            
                        // foreach (var quotaCount in passengerFromDb.ConPassengerQuotaCounts)
                        // {
                        //     quotaBalances.Add(new QuotBalanceDTO
                        //     {
                        //         Category = quotaCount.QuotaCategoriesCode,
                        //         Available = quotaCount.AvailableCount,
                        //         Issued = quotaCount.IssuedCount,
                        //         Refund = quotaCount.RefundCount,
                        //         Used = quotaCount.UsedCount
                        //     }); //MapDTO
                        // }
                        quotaBalance.category_balances = quotaBalances;
                        
                        quotaYearBalances.Add(quotaBalance);
                    }
                }
                passenger.quota_balances = quotaYearBalances;
                
                passengers.Add(passenger);
            }

            return new QueryResult(passengers);
        }
    }
}