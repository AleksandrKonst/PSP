using Application.DTO.PassengerContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.DocumentTypeQueries;

public static class GetDocumentTypeById
{
    public record Query(string Code) : IRequest<QueryResult>;
    
    public record QueryResult(DocumentTypeDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator(IPassengerTypeRepository repository)
        {
            RuleFor(x => x.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор типа документа не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IDocumentTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<DocumentTypeDTO>(await repository.GetByCodeAsync(request.Code)));
        }
    }
}