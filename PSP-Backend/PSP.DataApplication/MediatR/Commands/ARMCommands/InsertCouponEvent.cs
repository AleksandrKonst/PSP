using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.Domain.Models;
using PSP.Domain.Exceptions;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.ArmContextDTO.General;
using PSP.DataApplication.DTO.ArmContextDTO.Insert;
using PSP.DataApplication.DTO.ArmContextDTO.Insert.Validators;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.DataApplication.Infrastructure;
using PSP.DataApplication.MediatR.Commands.FareCommands;
using PSP.DataApplication.MediatR.Commands.FlightCommands;
using PSP.DataApplication.Mediatr.Commands.PassengerCommands;
using PSP.DataApplication.MediatR.Queries.ARMQueries;
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
            
            foreach (var coupon in request.PassengerRequestDto.Coupons)
            {
                foreach (var fare in coupon.Fares)
                {
                    var passengerData = request.PassengerRequestDto.Passengers.FirstOrDefault(p => p.Id == fare.PassengerId);
                    
                    if (passengerData == null)
                    {
                        throw new ResponseException("PFC-000004", "Для пассажира передано больше одного тарифа в купоне");
                    }
                
                    if (!await passengerRepository
                            .CheckByFullNameAsync(passengerData.Name, passengerData.Surname, passengerData.Patronymic, passengerData.Gender, passengerData.Birthdate))
                    {
                        var command = new CreatePassenger.Command(new PassengerDTO()
                        {
                            Birthdate = passengerData.Birthdate,
                            Gender = passengerData.Gender,
                            Name = passengerData.Name,
                            Surname = passengerData.Surname,
                            Patronymic = passengerData.Patronymic,
                            PassengerTypes = new List<string>()
                        });
                        var createResult = await mediator.Send(command, cancellationToken);

                        if (!createResult.Result)
                        {
                            throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера");
                        }
                    }
                    
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
                            opt.AfterMap((_, dest) => dest.FareCode = fare.Code);
                        }));
                        
                        var createResult = await mediator.Send(command, cancellationToken);

                        if (!createResult.Result)
                        {
                            throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера: Полет не создан");
                        }
                    }
                    
                    var passengerDb = await passengerRepository.GetByFullNameWithCouponEventAsync(passengerData.Name,
                        passengerData.Surname, passengerData.Patronymic, passengerData.Gender, passengerData.Birthdate, passengerData.QuotaBalancesYears);
                    
                    if (passengerDb == null)
                    {
                        throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера");
                    }
                    
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
                    
                    var passengerType = await passengerTypeRepository.GetByCodeAsync(fare.PassengerType);
                    if (passengerType == null) throw new ResponseException("PFC-000500", "Внутренняя ошибка сервера");
                    
                    var quotaCategory = passengerType.QuotaCategories.First();

                    var categoryBalance = passengerDb.CouponEvents.Count(c => c.QuotaCode == quotaCategory && c.OperationDatetimeUtc.Year ==
                            DateTime.Parse(request.PassengerRequestDto.OperationDatetime).ToUniversalTime().Year && c.OperationType == "used");

                    if ((request.PassengerRequestDto.OperationType != "refund" && request.PassengerRequestDto.OperationType != "edit" && request.PassengerRequestDto.OperationType != "exchange" || passengerDb.CouponEvents.Any(c => c.OperationType == "issued" && 
                            c.PassengerId == passengerDb.Id && c.FlightCode == coupon.FlightNumber && c.TicketNumber == passengerData.TicketNumber)) == false)
                    {
                        throw new ResponseException("PFC-000013", "Билет отсутствует");
                    }
                    
                    if ((categoryBalance < 4 || fare.PassengerType == "ocean") && passengerDb.CouponEvents
                            .Any(c => c.OperationType == request.PassengerRequestDto.OperationType && 
                                      c.PassengerId == passengerDb.Id && c.FlightCode == coupon.FlightNumber && c.TicketNumber == passengerData.TicketNumber) == false)
                    {
                        passengerDb.CouponEvents.Add(new CouponEvent() {
                            OperationType = request.PassengerRequestDto.OperationType,
                            OperationDatetimeUtc = DateTime.Parse(request.PassengerRequestDto.OperationDatetime).ToUniversalTime(),
                            OperationDatetimeTimezone = (short)DateTimeOffset.Parse(request.PassengerRequestDto.OperationDatetime).Offset.Hours,
                            OperationPlace = request.PassengerRequestDto.OperationPlace,
                            PassengerId = passengerDb.Id,
                            DocumentTypeCode = passengerData.DocumentType,
                            DocumentNumber = passengerData.DocumentNumber,
                            DocumentNumberLatin = ConvertStringService.Transliterate(passengerData.DocumentNumber),
                            QuotaCode = quotaCategory,
                            FlightCode = coupon.FlightNumber,
                            TicketType = passengerData.TicketType,
                            TicketNumber = passengerData.TicketNumber
                        });
                        await passengerRepository.UpdateAsync(passengerDb);
    
                        var quotaCategories = await quotaCategoryRepository.GetAllAsync();
                        
                        var quotaBalances = (from quotaYear in passengerData.QuotaBalancesYears
                            let categoryBalances = quotaCategories.Select(category => new CategoryBalanceDTO
                                {
                                    Category = category.Code,
                                    Available = 4 - passengerDb.CouponEvents.Count(dc => dc.OperationType == "used" && dc.QuotaCode == category.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                    Issued = passengerDb.CouponEvents.Count(dc => dc.OperationType == "issued" && dc.QuotaCode == category.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                    Refund = passengerDb.CouponEvents.Count(dc => dc.OperationType == "refund" && dc.QuotaCode == category.Code && dc.OperationDatetimeUtc.Year == quotaYear),
                                    Used = passengerDb.CouponEvents.Count(dc => dc.OperationType == "used" && dc.QuotaCode == category.Code && dc.OperationDatetimeUtc.Year == quotaYear)
                                })
                                .ToList()
                            select new InsertQuotaBalanceDTO()
                            {
                                Year = quotaYear, 
                                UsedDocumentCount = passengerDb.CouponEvents.Select(p => p.DocumentNumber).Distinct().Count(), 
                                Changed = false, 
                                CategoryBalances = categoryBalances
                            }).ToList();
                        
                        quotaBalances.First(q => q.Year == DateTime.Parse(request.PassengerRequestDto.OperationDatetime).ToUniversalTime().Year).Changed = true;
                        
                        passengers.Add(new InsertPassengerResponseDTO()
                        {
                            Id = passengerDb.Id,
                            TicketProperties = new InsertTicketPropertiesDTO()
                            {
                                PassengerTypesPreConfirmed = true,
                                ContainsQuotaRoutes = request.PassengerRequestDto.Coupons.Count > 0
                            },
                            QuotaBalances = quotaBalances
                        });
                    }
                    else
                    {
                        throw new ResponseException("PFC-000014", "Доступные квоты отсутствуют");
                    }
                }
            }
            return new CommandResult(passengers);
        }
    }
}