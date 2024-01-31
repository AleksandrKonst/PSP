using Application.DTO.PassengerContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.PassengerTypeQueries;

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