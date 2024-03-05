using Application.DTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Queries;

public static class GetSortedRoute
{
    public record Query(string departPlaceName, string arrivePlaceName, DateOnly Date) : IRequest<QueryResult>;
    
    public record QueryResult(IEnumerable<FlightViewModel> Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.arrivePlaceName)
                .MaximumLength(20)
                .WithMessage("Длинное название города")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.departPlaceName)
                .MaximumLength(20)
                .WithMessage("Длинное название города")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(IFlightRepository repository, IMapper mapper, ILogger<Handler> logger)
        : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var flightList = new List<List<Flight>>();
            var flightQueue = new Queue<List<Flight>>();
            
            var flights = await repository.GetAllForClientAsync(request.arrivePlaceName, request.departPlaceName, request.Date.ToDateTime(new TimeOnly()).ToUniversalTime());
            
            foreach (var flight in flights.Where(f => f.DepartPlaceNavigation.CityIataCodeNavigation.Name == request.departPlaceName))
            {
                flightQueue.Enqueue(new List<Flight> { flight });
            }
            
            while (flightQueue.Count > 0)
            {
                var currentFlight = flightQueue.Dequeue();
                var lastAirport = currentFlight.Last().ArrivePlaceNavigation.CityIataCodeNavigation.Name;

                if (lastAirport == request.arrivePlaceName)
                {
                    flightList.Add(currentFlight);
                }
                else
                {
                    foreach (var nextFlight in flights.Where(f => f.DepartPlaceNavigation.CityIataCodeNavigation.Name == lastAirport))
                    {
                        if (!currentFlight.Any(f =>
                                f.DepartPlace == nextFlight.DepartPlace && f.ArrivePlace == nextFlight.ArrivePlace))
                        {
                            var newRoute = new List<Flight>(currentFlight) { nextFlight };
                            flightQueue.Enqueue(newRoute);
                        }
                    }
                }
            }

            var result = flightList.Select(flight => new FlightViewModel()
                {
                    ArrivePlace = flight.Last().ArrivePlace,
                    ArriveDatetimePlan = flight.Last().ArriveDatetimePlan,
                    DepartPlace = flight.First().DepartPlace,
                    DepartDatetimePlan = flight.First().DepartDatetimePlan,
                    FareCode = flight.First().FareCode,
                    ArrivePlaceModel = mapper.Map<AirportViewModel>(flight.Last().ArrivePlaceNavigation),
                    DepartPlaceModel = mapper.Map<AirportViewModel>(flight.First().DepartPlaceNavigation),
                    Fare = mapper.Map<FareViewModel>(flight.First().FareCodeNavigation),
                    FlightSegments = mapper.Map<ICollection<FlightSegmentViewModel>>(flight)
                })
                .OrderBy(f => f.ArriveDatetimePlan - f.DepartDatetimePlan)
                .ThenBy(f => f.FlightSegments.Count)
                .ThenBy(f => f.Fare.Amount)
                .ToList();
            
            logger.LogInformation("Route send");
            return new QueryResult(result);
        }
    }
}