using AutoMapper;
using MediatR;
using FluentValidation;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;
using PSP.RouteApplication.DTO;

namespace PSP.RouteApplication.MediatR.Queries;

public static class GetSortedRoute
{
    public record Query(string arrivePlaceName, string departPlaceName, DateTime Date) : IRequest<QueryResult>;
    
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
    
    public class Handler(IFlightRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<FlightViewModel>>(
                await repository.GetAllForClientAsync(request.arrivePlaceName, request.departPlaceName, request.Date)));
        }
    }
}