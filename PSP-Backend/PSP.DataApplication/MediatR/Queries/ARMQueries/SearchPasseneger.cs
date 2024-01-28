using System.Dynamic;
using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO.ArmContextDTO.General;
using PSP.DataApplication.DTO.ArmContextDTO.Search;
using PSP.DataApplication.DTO.ArmContextDTO.Select;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.DataApplication.Inrastructure;
using PSP.Domain.Exceptions;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.ARMQueries;

public static class SearchPasseneger
{
    public record Query(SearchPassengerDTO SearchPassengerDto) : IRequest<QueryResult>;
    
    public record QueryResult(dynamic Result);
    
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            
        }
    }
    
    public class Handler(IPassengerRepository passengerRepository, IQuotaCategoryRepository quotaCategoryRepository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            dynamic passenger = new ExpandoObject();
                
            var passengerFromDb = await passengerRepository
                .GetByFullNameWithCouponEventAsync(request.SearchPassengerDto.Name, request.SearchPassengerDto.Surname, request.SearchPassengerDto.Patronymic, 
                    request.SearchPassengerDto.Birthdate, new List<int>() {request.SearchPassengerDto.QuotaBalancesYear});

            if (passengerFromDb != null)
            {
                passenger.passengerData = new SelectPassengerDataDTO()
                {
                    //mapper
                    Birthdate = passengerFromDb.Birthdate,
                    Gender = passengerFromDb.Gender,
                    DocumentType = request.SearchPassengerDto.DocumentType,
                    DocumentNumber = request.SearchPassengerDto.DocumentNumber,
                    DocumentNumbersLatin = new List<string>()
                        { ConvertStringService.Transliterate(request.SearchPassengerDto.DocumentNumber) }
                };
                
                var quotaBalances = new List<SelectQuotaBalanceDTO>();
                
                var quotaBalance = new SelectQuotaBalanceDTO();
                quotaBalance.Year = request.SearchPassengerDto.QuotaBalancesYear;
                quotaBalance.UsedDocumentsCount = passengerFromDb.CouponEvents.Select(p => p.DocumentNumber).Distinct().Count() + 1;

                var categoryBalances = new List<CategoryBalanceDTO>();
                var quotaCategories = await quotaCategoryRepository.GetAllAsync();
                
                foreach (var quotaCategory in quotaCategories)
                {
                    categoryBalances.Add(new CategoryBalanceDTO
                    {
                        Category = quotaCategory.Code,
                        Available = 4 - passengerFromDb.CouponEvents
                            .Count(dc => dc.OperationType == "used" &&
                                         dc.QuotaCode == quotaCategory.Code &&
                                         dc.OperationDatetimeUtc.Year == request.SearchPassengerDto.QuotaBalancesYear),
                        Issued = passengerFromDb.CouponEvents
                            .Count(dc => dc.OperationType == "issued" &&
                                         dc.QuotaCode == quotaCategory.Code &&
                                         dc.OperationDatetimeUtc.Year == request.SearchPassengerDto.QuotaBalancesYear),
                        Refund = passengerFromDb.CouponEvents
                            .Count(dc => dc.OperationType == "refund" &&
                                         dc.QuotaCode == quotaCategory.Code &&
                                         dc.OperationDatetimeUtc.Year == request.SearchPassengerDto.QuotaBalancesYear),
                        Used = passengerFromDb.CouponEvents
                            .Count(dc => dc.OperationType == "used" &&
                                         dc.QuotaCode == quotaCategory.Code &&
                                         dc.OperationDatetimeUtc.Year == request.SearchPassengerDto.QuotaBalancesYear)
                    });
                }

                quotaBalance.categoryBalances = categoryBalances;
                quotaBalances.Add(quotaBalance);
                passenger.QuotaBalances = quotaBalances; 
                
                passenger.couponEvents = mapper.Map<List<CouponEventDTO>>(passengerFromDb.CouponEvents.Where(c => c.DocumentTypeCode == request.SearchPassengerDto.DocumentType && 
                    c.DocumentNumber == request.SearchPassengerDto.DocumentNumber));
            }
            else
            {
                throw new ResponseException("PPC-000001", "Идентификатор пассажира не существует");
            }

            return new QueryResult(passenger);
        }
    }
}