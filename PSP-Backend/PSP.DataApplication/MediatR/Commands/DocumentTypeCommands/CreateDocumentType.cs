using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.PassengerRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.DocumentTypeCommands;

public static class CreateDocumentType
{
    public record Command(DocumentTypeDTO DocumentTypeDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IPassengerTypeRepository repository)
        {
            RuleFor(x => x.DocumentTypeDto.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор типа документа")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(IDocumentTypeRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<DocumentType>(request.DocumentTypeDto)));
        }
    }
}