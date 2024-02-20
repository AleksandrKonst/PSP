using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.AirportQueries;

public static class GetAirportById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(AirportDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IAirportRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор типа документа не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IAirportRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<AirportDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}