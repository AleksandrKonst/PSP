using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.AirlineQueries;

public static class GetAirlineById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(AirlineDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IAirlineRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор перевочика не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IAirlineRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<AirlineDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}