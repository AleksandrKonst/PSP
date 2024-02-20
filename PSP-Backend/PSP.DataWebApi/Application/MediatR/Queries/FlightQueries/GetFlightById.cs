using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.FlightQueries;

public static class GetFlightById
{
    public record Query(long Code) : IRequest<QueryResult>;
    
    public record QueryResult(FlightDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IFlightRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор перелета не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IFlightRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<FlightDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}