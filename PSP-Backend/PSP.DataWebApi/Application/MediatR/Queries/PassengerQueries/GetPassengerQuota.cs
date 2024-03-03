using Application.DTO.ArmContextDTO.General;
using Application.DTO.ArmContextDTO.Select;
using Application.DTO.PassengerContextDTO;
using AutoMapper;
using Domain.Exceptions;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.PassengerQueries;

public static class GetPassengerQuota
{
    public record Query(string Name, string Surname, string Patronymic, DateOnly Birthdate, int QuotaBalancesYear) : IRequest<QueryResult>;
    
    public record QueryResult(PassengerQuotaDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.QuotaBalancesYear)
                .LessThan(9999)
                .GreaterThan(2000)
                .WithMessage("Неверный формат года")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.Surname)
                .MaximumLength(40)
                .WithMessage("Длинная фамилия")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.Name)
                .MaximumLength(40)
                .WithMessage("Длинное имя")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.Patronymic)
                .MaximumLength(40)
                .WithMessage("Длинное отчество")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.Birthdate.Year)
                .LessThan(9999)
                .GreaterThan(1000)
                .WithMessage("Неверный формат года")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(IPassengerRepository passengerRepository, IQuotaCategoryRepository quotaCategoryRepository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var passenger = new PassengerQuotaDTO();
                
            var passengerFromDb = await passengerRepository
                .GetByFullNameWithCouponEventAsync(request.Name, request.Surname, request.Patronymic, request.Birthdate, 
                    new List<int>() {request.QuotaBalancesYear});

            if (passengerFromDb != null)
            {
                var quotaBalances = new List<SelectQuotaBalanceDTO>();
                
                var quotaBalance = new SelectQuotaBalanceDTO
                {
                    Year = request.QuotaBalancesYear,
                    UsedDocumentsCount = passengerFromDb.CouponEvents.Select(p => p.DocumentNumber).Distinct().Count()
                };

                var quotaCategories = await quotaCategoryRepository.GetAllAsync();
                var categoryBalances = quotaCategories.Select(quotaCategory => new CategoryBalanceDTO
                    {
                        Category = quotaCategory.Code,
                        Available = 4 - passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "used" && 
                            dc.QuotaCode == quotaCategory.Code && 
                            dc.OperationDatetimeUtc.Year == request.QuotaBalancesYear),
                        Issued = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "issued" && 
                            dc.QuotaCode == quotaCategory.Code && 
                            dc.OperationDatetimeUtc.Year == request.QuotaBalancesYear),
                        Refund = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "refund" && 
                            dc.QuotaCode == quotaCategory.Code && 
                            dc.OperationDatetimeUtc.Year == request.QuotaBalancesYear),
                        Used = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "used" && 
                            dc.QuotaCode == quotaCategory.Code && 
                            dc.OperationDatetimeUtc.Year == request.QuotaBalancesYear)
                    })
                    .ToList();
                
                quotaBalance.CategoryBalances = categoryBalances;
                quotaBalances.Add(quotaBalance);
                
                passenger.QuotaBalances = quotaBalances; 
                passenger.CouponEvents = mapper.Map<List<PassangerCouponEventDTO>>(passengerFromDb.CouponEvents);
            }
            else
            {
                throw new ResponseException("PPC-000001", "Идентификатор пассажира не существует");
            }
            return new QueryResult(passenger);
        }
    }
}