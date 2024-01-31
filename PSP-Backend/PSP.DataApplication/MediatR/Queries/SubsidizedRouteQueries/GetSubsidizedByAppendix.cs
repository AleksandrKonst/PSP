using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.SubsidizedRouteQueries;

public class GetSubsidizedByAppendix
{
    public record Query(short Appendix) : IRequest<QueryResult>;

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Appendix)
                .LessThan((short) 6)
                .GreaterThan((short)0)
                .WithMessage("Неверный индекс приложения")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public record QueryResult(IEnumerable<SubsidizedCityDTO> Result);
    
    public class Handler(ISubsidizedRouteRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<SubsidizedCityDTO>>(await repository.GetAllByAppendixAsync(request.Appendix)));
        }
    }
}