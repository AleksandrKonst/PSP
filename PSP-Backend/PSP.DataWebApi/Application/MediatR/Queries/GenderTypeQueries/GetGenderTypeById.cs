using Application.DTO.PassengerContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.GenderTypeQueries;

public static class GetGenderTypeById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(GenderTypeDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IGenderTypeRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Такой пол не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IGenderTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<GenderTypeDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}