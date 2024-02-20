using Application.DTO.FlightContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.CityQueries;

public class GetCityById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(CityDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(ICityRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор города не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(ICityRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<CityDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}