using System.Dynamic;
using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.ArmContextDTO.Select;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.ARMQueries;

public static class SelectPassengerQuotaCount
{
    public record Query(IEnumerable<SelectPassengerRequestDTO> SelectPassengerRequests) : IRequest<QueryResult>;
    
    public record QueryResult(List<dynamic> Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IPassengerRepository repository)
        {
            RuleFor(x => x.SelectPassengerRequests)
                .NotEmpty()
                .WithMessage("Передана пустая коллекция")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(IPassengerRepository passengerRepository, IQuotaCategoryRepository quotaCategoryRepository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var quotaCategories = await quotaCategoryRepository.GetAllAsync();
            var passengers = new List<dynamic>();
            
            foreach (var requestDto in request.SelectPassengerRequests)
            {
                dynamic passenger = new ExpandoObject();
                passenger.id = requestDto.Id;

                if (await passengerRepository.CheckByFullNameAsync(requestDto.Name, requestDto.Surname, requestDto.Patronymic, requestDto.Birthdate))
                {
                    var passengerFromDb = await passengerRepository
                        .GetByIdWithCouponEventAsync(requestDto.Name, requestDto.Surname, requestDto.Patronymic, requestDto.Birthdate, requestDto.QuotaBalancesYears);
                    
                    passenger.passenger_data = mapper.Map<SelectPassengerDataDTO>(passengerFromDb);
                    //Mock go to url and get Confirme
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
                
                            var categoryBalances = new List<CategoryBalanceDTO>();
                            
                            foreach (var quotaCategory in quotaCategories)
                            {
                                categoryBalances.Add(new CategoryBalanceDTO
                                {
                                    Category = quotaCategory.Code,
                                    Available = 4 - passengerFromDb.DataCouponEvents
                                        .Count(dc => dc.OperationType == "used" && 
                                                     dc.Fare.QuotaCategoryCode == quotaCategory.Code && 
                                                     dc.OperationDatetimeUtc.Year == quotaYear),
                                    Issued = passengerFromDb.DataCouponEvents
                                        .Count(dc => dc.OperationType == "issued" && 
                                                     dc.Fare.QuotaCategoryCode == quotaCategory.Code && 
                                                     dc.OperationDatetimeUtc.Year == quotaYear),
                                    Refund = passengerFromDb.DataCouponEvents
                                        .Count(dc => dc.OperationType == "refund" && 
                                                     dc.Fare.QuotaCategoryCode == quotaCategory.Code && 
                                                     dc.OperationDatetimeUtc.Year == quotaYear),
                                    Used = passengerFromDb.DataCouponEvents
                                        .Count(dc => dc.OperationType == "used" && 
                                                     dc.Fare.QuotaCategoryCode == quotaCategory.Code && 
                                                     dc.OperationDatetimeUtc.Year == quotaYear)
                                });
                            }
                            quotaBalance.category_balances = categoryBalances;
                            
                            quotaYearBalances.Add(quotaBalance);
                        }
                    }
                    passenger.quota_balances = quotaYearBalances;
                }
                else
                {
                    passenger.identity_confirmation = new
                    {
                        Confirmed = false,
                        Code = "PFC-000006",
                        Message = "Передан идентификатор отсутствующего пассажира"
                    };
                }
                
                passengers.Add(passenger);
            }

            return new QueryResult(passengers);
        }
    }
}