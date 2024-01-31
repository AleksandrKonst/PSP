using System.Dynamic;
using Application.DTO.ArmContextDTO.General;
using Application.DTO.ArmContextDTO.Select;
using Application.DTO.ArmContextDTO.Select.Validators;
using Application.DTO.PassengerContextDTO;
using Application.MediatR.Commands.PassengerCommands;
using AutoMapper;
using Domain.Exceptions;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.ARMQueries;

public static class SelectPassengerQuotaCount
{
    public record Query(IEnumerable<SelectPassengerRequestDTO> SelectPassengerRequests) : IRequest<QueryResult>;
    
    public record QueryResult(List<dynamic> Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.SelectPassengerRequests)
                .NotEmpty()
                .WithMessage("Передана пустая коллекция")
                .WithErrorCode("PPC-000403");

            RuleForEach(x => x.SelectPassengerRequests).SetValidator(new SelectPassengerValidator());
        }
    }
    
    public class Handler(IPassengerRepository passengerRepository, IQuotaCategoryRepository quotaCategoryRepository, 
        IMediator mediator, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var quotaCategories = await quotaCategoryRepository.GetAllAsync();
            var passengers = new List<dynamic>();
            
            foreach (var passengerRequest in request.SelectPassengerRequests)
            {
                dynamic passenger = new ExpandoObject();
                passenger.id = passengerRequest.Id;
                
                var passengerFromDb = await passengerRepository
                    .GetByFullNameWithCouponEventAsync(passengerRequest.Name, passengerRequest.Surname, passengerRequest.Patronymic, passengerRequest.Gender, 
                        passengerRequest.Birthdate, passengerRequest.QuotaBalancesYears);

                if (passengerFromDb != null)
                {
                    passenger.passengerData = mapper.Map<SelectPassengerDataDTO>(passengerRequest);
                    
                    var checkPassengerQuery= new CheckPassenger.Query(passengerRequest.Name, passengerRequest.Surname, passengerRequest.Patronymic, passengerRequest.Birthdate);
                    var checkPassengerResult = await mediator.Send(checkPassengerQuery, cancellationToken);
                                
                    if (checkPassengerResult.Result)
                    {
                        passenger.identityConfirmation = new
                        {
                            Confirmed = true,
                            Code = "PIC-000000",
                            Message = "Успешное подтверждение личности гражданина"
                        };
                    }
                    else
                    {
                        passenger.identityConfirmation = new
                        {
                            Confirmed = true,
                            Code = "PIC-000001",
                            Message = "Ошибка подтверждения личности гражданина"
                        };
                    }

                    var typeConfirmations = new List<SelectTypeConfirmationDTO>();
                    if (passengerRequest.Types.Count > 0)
                    {
                        foreach (var type in passengerRequest.Types)
                        {
                            SelectTypeConfirmationDTO selectTypeConfirmation;
                            var confirmed = true;

                            if (!passengerFromDb.PassengerTypes.Contains(type))
                            {
                                var query = new CheckPassengerType.Query(passengerRequest.Name, passengerRequest.Surname, passengerRequest.Patronymic, passengerRequest.Birthdate);
                                var result = await mediator.Send(query, cancellationToken);
                                
                                if (result.Result)
                                {
                                    passengerFromDb.PassengerTypes.Add(type);
                                    await passengerRepository.UpdateAsync(passengerFromDb);
                                }
                                confirmed = result.Result;
                            }
                            
                            if (confirmed)
                            {
                                selectTypeConfirmation = new SelectTypeConfirmationDTO()
                                {
                                    Status = "confirmed",
                                    Code = "PTC-000000",
                                    Message = "Успешное подтверждение типа пассажира"
                                };
                            }
                            else
                            {
                                selectTypeConfirmation = new SelectTypeConfirmationDTO()
                                {
                                    Status = "not confirmed",
                                    Code = "PTC-000001",
                                    Message = "Тип пассажира не подтвержден"
                                }; 
                            }

                            typeConfirmations.Add(selectTypeConfirmation);
                        }
                    }

                    passenger.typeConfirmations = typeConfirmations;
                    if (passengerRequest.QuotaBalancesYears.Count > 0)
                    {
                        var quotaBalances = new List<SelectQuotaBalanceDTO>();
                        foreach (var quotaYear in passengerRequest.QuotaBalancesYears)
                        {
                            var quotaBalance = new SelectQuotaBalanceDTO
                            {
                                Year = quotaYear,
                                UsedDocumentsCount = passengerFromDb.CouponEvents.Select(p => p.DocumentNumber).Distinct().Count()
                            };

                            var categoryBalances = quotaCategories.Select(quotaCategory => new CategoryBalanceDTO
                                {
                                    Category = quotaCategory.Code,
                                    Available = 4 - passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "used" && 
                                        dc.QuotaCode == quotaCategory.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                    Issued = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "issued" && 
                                        dc.QuotaCode == quotaCategory.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                    Refund = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "refund" && 
                                        dc.QuotaCode == quotaCategory.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                    Used = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "used" && 
                                        dc.QuotaCode == quotaCategory.Code && dc.OperationDatetimeUtc.Year == quotaYear)
                                })
                                .ToList();

                            quotaBalance.CategoryBalances = categoryBalances;
                            quotaBalances.Add(quotaBalance);
                        }
                        passenger.QuotaBalances = quotaBalances; 
                    }
                }
                else
                {
                    var command =  new CreatePassenger.Command(new PassengerDTO()
                    {
                        Birthdate = passengerRequest.Birthdate,
                        Gender = passengerRequest.Gender,
                        Name = passengerRequest.Name,
                        Surname = passengerRequest.Surname,
                        Patronymic = passengerRequest.Patronymic,
                        PassengerTypes = passengerRequest.Types
                    });
                    var createResult = await mediator.Send(command, cancellationToken);
                    
                    if (createResult.Result)
                    {
                        passenger.passengerData = mapper.Map<SelectPassengerDataDTO>(passengerRequest);

                        if (passengerRequest.QuotaBalancesYears.Count > 0)
                        {
                            var quotaBalances = passengerRequest.QuotaBalancesYears.Select(quotaYear => new SelectQuotaBalanceDTO
                                {
                                    Year = quotaYear,
                                    UsedDocumentsCount = 1,
                                    CategoryBalances = quotaCategories.Select(quotaCategory => new CategoryBalanceDTO
                                        {
                                            Category = quotaCategory.Code,
                                            Available = 4,
                                            Issued = 0,
                                            Refund = 0,
                                            Used = 0
                                        }).ToList()
                                }).ToList();
                            passenger.quotaBalances = quotaBalances;  
                        }
                    }
                    else
                    {
                        throw new ResponseException("Ошибка добавления пассажира", "PPC-000500");
                    }
                }
                passengers.Add(passenger);
            }

            return new QueryResult(passengers);
        }
    }
}