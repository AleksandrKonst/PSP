using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.DataApplication.DTO.PassengerContextDTO;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.PassengerTypeQueries;

public static class GetPassengerTypeById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(PassengerTypeDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IPassengerTypeRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор типа пассажира не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IPassengerTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<PassengerTypeDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}