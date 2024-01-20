using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Queries.PassengerTypeQueries;

public static class GetPassengerTypeById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(PassengerTypeDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty()
                .WithMessage("Неверный формат данных");
        }
    }
    
    public class Handler(IPassengerTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<PassengerTypeDTO>(await repository.GetByIdAsync(request.Code)));
        }
    }
}