using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.ArmContextDTO.Insert;
using PSP.DataApplication.DTO.ArmContextDTO.Select;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.DataApplication.MediatR.Commands.CouponEventCommands;
using PSP.Domain.Exceptions;
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
            
        }
    }
    
    public class Handler(IPassengerRepository passengerRepository, IQuotaCategoryRepository quotaCategoryRepository, 
        IFlightRepository flightRepository, IMapper mapper, IMediator mediator) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            var passengers = new List<InsertPassengerResponseDTO>();
            var passengersId = new Dictionary<int, long>();
            var passengersTicketType = new Dictionary<int, short>();
            
            foreach (var passenger in request.PassengerRequestDto.Passengers)
            {
                var quotaCategories = await quotaCategoryRepository.GetAllAsync();
                
                var passengerFromDb = await passengerRepository
                    .GetByIdWithCouponEventAsync(passenger.Name, passenger.Surname, passenger.Patronymic, 
                        passenger.Birthdate, passenger.QuotaBalancesYears);
                
                if (passengerFromDb == null) throw new ResponseException("1", "2");

                passengersId.Add(passenger.Id, passengerFromDb.Id);
                passengersTicketType.Add(passenger.Id, passenger.TicketType);
                
                var quotaBalances = new List<QuotaBalanceDTO>();
                
                foreach (var quotaYear in passenger.QuotaBalancesYears)
                {
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
                    
                    quotaBalances.Add(new QuotaBalanceDTO()
                    {
                        Year = quotaYear,
                        UsedDocumentCount = 1,
                        Changed = false,
                        CategoryBalances = categoryBalances
                    });
                }
                
                passengers.Add(new InsertPassengerResponseDTO()
                {
                    Id = passenger.Id,
                    TicketProperties = new TicketPropertiesDTO()
                    {
                        PassengerTypesPreConfirmed = true,
                        ContainsQuotaRoutes = request.PassengerRequestDto.Coupons.Count > 0
                    },
                    QuotaBalances = quotaBalances
                });
            }
            
            foreach (var coupon in request.PassengerRequestDto.Coupons)
            {
                foreach (var fare in coupon.Fares)
                {
                    //check fare
                    
                    //check count event
                    
                    // if (await flightRepository.CheckByCodeAsync(coupon.FlightNumber))
                    // {
                    //     throw new ResponseException("1", "2");
                    // }
                    
                    if (passengersId.ContainsKey(fare.PassengerId))
                    {
                        var command = new CreateCouponEvent.Command(new CouponEventDTO()
                        {
                            OperationType = request.PassengerRequestDto.OperationType,
                            OperationDatetimeUtc = DateTime.Parse(request.PassengerRequestDto.OperationDatetime).ToUniversalTime(),
                            OperationDatetimeTimezone = (short)DateTimeOffset.Parse(request.PassengerRequestDto.OperationDatetime).Offset.Hours,
                            OperationPlace = request.PassengerRequestDto.OperationPlace,
                            PassengerId = passengersId.GetValueOrDefault(fare.PassengerId),
                            TicketType = passengersTicketType.GetValueOrDefault(fare.PassengerId),
                            FlightCode = coupon.FlightNumber,
                            FareCode = fare.Code
                        });
                        var result = await mediator.Send(command, cancellationToken);

                        if (result.Result)
                        {
                            var quotaBalance = passengers
                                .First(p => p.Id == fare.PassengerId)
                                .QuotaBalances
                                .First(q => q.Year == DateTime.Parse(request.PassengerRequestDto.OperationDatetime).ToUniversalTime().Year);

                            quotaBalance.Changed = true;
                        }
                    }
                    else
                    {
                        throw new ResponseException("1", "2");
                    }
                    
                }
            }

            return new CommandResult(passengers);
        }
    }
}