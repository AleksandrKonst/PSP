using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.ArmContextDTO.General;
using PSP.DataApplication.DTO.ArmContextDTO.Insert;
using PSP.DataApplication.DTO.ArmContextDTO.Insert.Validators;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.DataApplication.Infrastructure;
using PSP.DataApplication.MediatR.Commands.CouponEventCommands;
using PSP.DataApplication.MediatR.Commands.FareCommands;
using PSP.DataApplication.MediatR.Commands.FlightCommands;
using PSP.DataApplication.Mediatr.Commands.PassengerCommands;
using PSP.DataApplication.MediatR.Queries.ARMQueries;
using PSP.Domain.Exceptions;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.ARMCommands;

public static class InsertCouponEvent
{
    public record Command(InsertPassengerRequestDTO PassengerRequestDto) : IRequest<CommandResult>;
    
    public record CommandResult(List<InsertPassengerResponseDTO> Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleForEach(x => x.PassengerRequestDto.Coupons).SetValidator(new InsertCouponValidator());
            RuleForEach(x => x.PassengerRequestDto.Passengers).SetValidator(new InsertPassengerDataValidator());
        }
    }
    
    public class Handler(IPassengerRepository passengerRepository, IQuotaCategoryRepository quotaCategoryRepository, 
        IFlightRepository flightRepository, IFareRepository fareRepository, IPassengerTypeRepository passengerTypeRepository, IMapper mapper, IMediator mediator) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var passengers = new List<InsertPassengerResponseDTO>();
            var passengersDb = new Dictionary<int, Passenger>();
            
            foreach (var passenger in request.PassengerRequestDto.Passengers)
            {
                var quotaCategories = await quotaCategoryRepository.GetAllAsync();
                
                if (!await passengerRepository
                        .CheckByFullNameAsync(passenger.Name, passenger.Surname, passenger.Patronymic, passenger.Birthdate))
                {
                    var command = new CreatePassenger.Command(new PassengerDTO()
                    {
                        Birthdate = passenger.Birthdate,
                        Gender = passenger.Gender,
                        Name = passenger.Name,
                        Surname = passenger.Surname,
                        Patronymic = passenger.Patronymic,
                        PassengerTypes = new List<string>()
                    });
                    var createResult = await mediator.Send(command, cancellationToken);

                    if (!createResult.Result)
                    {
                        throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера");
                    }
                }
                
                var passengerFromDb = await passengerRepository
                    .GetByFullNameWithCouponEventAsync(passenger.Name, passenger.Surname, passenger.Patronymic, 
                        passenger.Birthdate, passenger.QuotaBalancesYears);

                if (passengerFromDb != null)
                {
                    passengersDb.Add(passenger.Id, passengerFromDb);

                    var quotaBalances = (from quotaYear in passenger.QuotaBalancesYears
                        let categoryBalances = quotaCategories.Select(quotaCategory => new CategoryBalanceDTO
                            {
                                Category = quotaCategory.Code,
                                Available = 4 - passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "used" && dc.QuotaCode == quotaCategory.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                Issued = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "issued" && dc.QuotaCode == quotaCategory.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                Refund = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "refund" && dc.QuotaCode == quotaCategory.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                Used = passengerFromDb.CouponEvents.Count(dc => dc.OperationType == "used" && dc.QuotaCode == quotaCategory.Code && dc.OperationDatetimeUtc.Year == quotaYear)
                            })
                            .ToList()
                        select new InsertQuotaBalanceDTO() { Year = quotaYear, UsedDocumentCount = passengerFromDb.CouponEvents.Select(p => p.DocumentNumber).Distinct().Count() + 1, Changed = false, CategoryBalances = categoryBalances }).ToList();

                    passengers.Add(new InsertPassengerResponseDTO()
                    {
                        Id = passenger.Id,
                        TicketProperties = new InsertTicketPropertiesDTO()
                        {
                            PassengerTypesPreConfirmed = false,
                            ContainsQuotaRoutes = request.PassengerRequestDto.Coupons.Count > 0
                        },
                        QuotaBalances = quotaBalances
                    });
                }
                else
                {
                    throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера");
                }
            }
            
            foreach (var coupon in request.PassengerRequestDto.Coupons)
            {
                foreach (var fare in coupon.Fares)
                {
                    if (!await fareRepository.CheckByCodeAsync(fare.Code))
                    {
                        var command = new CreateFare.Command(mapper.Map<FareDTO>(fare));
                        var createResult = await mediator.Send(command, cancellationToken);

                        if (!createResult.Result)
                        {
                            throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера: Тариф не создан");
                        }
                    }
                    
                    if (!await flightRepository.CheckByCodeAsync(coupon.FlightNumber))
                    {
                        var command = new CreateFlight.Command(mapper.Map<FlightDTO>(coupon, opt =>
                        {
                            opt.AfterMap((src, dest) => dest.FareCode = fare.Code);
                        }));
                        
                        var createResult = await mediator.Send(command, cancellationToken);

                        if (!createResult.Result)
                        {
                            throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера: Полет не создан");
                        }
                    }
                    
                    var passengerDb = passengersDb.GetValueOrDefault(fare.PassengerId);
                    
                    if (passengerDb != null)
                    {
                        if (!passengerDb.PassengerTypes.Contains(fare.PassengerType))
                        {
                            var query = new CheckPassengerType.Query(passengerDb.Name, passengerDb.Surname, passengerDb.Patronymic, passengerDb.Birthdate);
                            var result = await mediator.Send(query, cancellationToken);
                                
                            if (result.Result)
                            {
                                passengerDb.PassengerTypes.Add(fare.PassengerType);
                                await passengerRepository.UpdateAsync(passengerDb);
                            }
                            else
                            {
                                throw new ResponseException("PFC-000403", "Jшибка проверки типа");
                            }
                        }
                        
                        passengers.First(p => p.Id == fare.PassengerId).TicketProperties.PassengerTypesPreConfirmed = true;
                    }
                    else
                    {
                        throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера");
                    }
                    
                    if (passengersDb.ContainsKey(fare.PassengerId))
                    {
                        var passengerType = await passengerTypeRepository.GetByCodeAsync(fare.PassengerType);
                        if (passengerType == null) throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера");
                        
                        var quotaCategory = passengerType.QuotaCategories.First();
                        
                        var categoryBalance = passengers.First(p => p.Id == fare.PassengerId)
                            .QuotaBalances
                            .First(q => q.Year == DateTime.Parse(request.PassengerRequestDto.OperationDatetime).ToUniversalTime().Year)
                            .CategoryBalances
                            .First(c => c.Category == quotaCategory);
                        
                        if (fare.PassengerType == "ocean" || categoryBalance.Available > 0)
                        {
                            var passenger = request.PassengerRequestDto.Passengers.Find(p => p.Id == fare.PassengerId);
                            //check 
                            var command = new CreateCouponEvent.Command(new CouponEventDTO()
                            {
                                OperationType = request.PassengerRequestDto.OperationType,
                                OperationDatetimeUtc = DateTime.Parse(request.PassengerRequestDto.OperationDatetime).ToUniversalTime(),
                                OperationDatetimeTimezone = (short)DateTimeOffset.Parse(request.PassengerRequestDto.OperationDatetime).Offset.Hours,
                                OperationPlace = request.PassengerRequestDto.OperationPlace,
                                PassengerId = passengerDb.Id,
                                DocumentTypeCode = passenger.DocumentType,
                                DocumentNumber = passenger.DocumentNumber,
                                DocumentNumberLatin = ConvertStringService.Transliterate(passenger.DocumentNumber),
                                QuotaCode = quotaCategory,
                                FlightCode = coupon.FlightNumber,
                                TicketType = passenger.TicketType,
                                TicketNumber = passenger.TicketNumber
                            });
                            var result = await mediator.Send(command, cancellationToken);
            
                            if (result.Result)
                            {
                                var quotaBalance = passengers
                                    .ElementAt(fare.PassengerId - 1)
                                    .QuotaBalances
                                    .First(q => q.Year == DateTime.Parse(request.PassengerRequestDto.OperationDatetime).ToUniversalTime().Year);
            
                                quotaBalance.Changed = true;
                            }
                        }
                        else
                        {
                            throw new ResponseException("PFC-000014", "Доступные квоты отсутствуют");
                        }
                    }
                    else
                    {
                        throw new ResponseException("PFC-000006", "Для тарифа передан идентификатор отсутствующего пассажира");
                    }
                }
            }

            return new CommandResult(passengers);
        }
    }
}