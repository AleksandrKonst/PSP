using Application.DTO.ArmContextDTO.General;
using Application.DTO.ArmContextDTO.Search;
using Application.DTO.ArmContextDTO.Select;
using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Exceptions;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Queries.ARMQueries;

public static class SearchByPassenger
{
    public record Query(SearchByPassengerDTO SearchByPassengerDto) : IRequest<QueryResult>;
    
    public record QueryResult(SearchPassengerResponseDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.SearchByPassengerDto.QuotaBalancesYear)
                .LessThan(9999)
                .GreaterThan(2000)
                .WithMessage("Неверный формат года")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByPassengerDto.Surname)
                .MaximumLength(40)
                .WithMessage("Длинная фамилия")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByPassengerDto.Name)
                .MaximumLength(40)
                .WithMessage("Длинное имя")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByPassengerDto.Patronymic)
                .MaximumLength(40)
                .WithMessage("Длинное отчество")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByPassengerDto.Birthdate.Year)
                .LessThan(9999)
                .GreaterThan(1000)
                .WithMessage("Неверный формат года")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByPassengerDto.Gender)
                .Must(g => g is "M" or "F")
                .WithMessage("Неверный пол (M или F)")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByPassengerDto.DocumentType)
                .MaximumLength(2)
                .WithMessage("Длинный тип документа")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByPassengerDto.DocumentNumber)
                .MaximumLength(20)
                .WithMessage("Длинный номер документа")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.SearchByPassengerDto.Snils)
                .Length(11)
                .WithMessage("Неверный номер снилса")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(IPassengerRepository passengerRepository, IQuotaCategoryRepository quotaCategoryRepository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var passenger = new SearchPassengerResponseDTO();
                
            var passengerFromDb = await passengerRepository
                .GetByFullNameWithCouponEventAsync(request.SearchByPassengerDto.Name, request.SearchByPassengerDto.Surname, request.SearchByPassengerDto.Patronymic, request.SearchByPassengerDto.Gender, 
                    request.SearchByPassengerDto.Birthdate, new List<int>() {request.SearchByPassengerDto.QuotaBalancesYear});

            if (passengerFromDb != null)
            {
                passenger.PassengerData = mapper.Map<SelectPassengerDataDTO>(request.SearchByPassengerDto, opt =>
                {
                    opt.AfterMap((src, dest) => dest.Birthdate = passengerFromDb.Birthdate);
                    opt.AfterMap((src, dest) => dest.Gender = passengerFromDb.Gender);
                });
                
                var quotaBalances = new List<SelectQuotaBalanceDTO>();
                
                var quotaBalance = new SelectQuotaBalanceDTO
                {
                    Year = request.SearchByPassengerDto.QuotaBalancesYear,
                    UsedDocumentsCount = passengerFromDb.CouponEvents.Select(p => p.DocumentNumber).Distinct().Count()
                };

                var quotaCategories = await quotaCategoryRepository.GetAllAsync();
                var categoryBalances = quotaCategories.Select(quotaCategory => new CategoryBalanceDTO
                    {
                        Category = quotaCategory.Code,
                        Available = 4 - passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "used" && 
                            dc.QuotaCode == quotaCategory.Code && 
                            dc.OperationDatetimeUtc.Year == request.SearchByPassengerDto.QuotaBalancesYear),
                        Issued = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "issued" && 
                            dc.QuotaCode == quotaCategory.Code && 
                            dc.OperationDatetimeUtc.Year == request.SearchByPassengerDto.QuotaBalancesYear),
                        Refund = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "refund" && 
                            dc.QuotaCode == quotaCategory.Code && 
                            dc.OperationDatetimeUtc.Year == request.SearchByPassengerDto.QuotaBalancesYear),
                        Used = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "used" && 
                            dc.QuotaCode == quotaCategory.Code && 
                            dc.OperationDatetimeUtc.Year == request.SearchByPassengerDto.QuotaBalancesYear)
                    })
                    .ToList();
                
                quotaBalance.CategoryBalances = categoryBalances;
                quotaBalances.Add(quotaBalance);
                
                passenger.QuotaBalances = quotaBalances; 
                passenger.CouponEvents = mapper.Map<List<CouponEventDTO>>(passengerFromDb.CouponEvents
                    .Where(c => c.DocumentTypeCode == request.SearchByPassengerDto.DocumentType && 
                    c.DocumentNumber == request.SearchByPassengerDto.DocumentNumber));
            }
            else
            {
                throw new ResponseException("PPC-000001", "Идентификатор пассажира не существует");
            }
            logger.LogInformation($"Search {nameof(SearchByPassenger)}");
            return new QueryResult(passenger);
        }
    }
}