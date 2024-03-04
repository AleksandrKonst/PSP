using Application.DTO.PassengerContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.DocumentTypeCommands;

public static class UpdateDocumentType
{
    public record Command(DocumentTypeDTO DocumentTypeDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IDocumentTypeRepository repository)
        {
            RuleFor(x => x.DocumentTypeDto.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор типа документа не существует")
                .WithErrorCode("PPC-000001");
            
            RuleFor(x => x.DocumentTypeDto.Code)
                .MaximumLength(2)
                .WithMessage("Идентификатор типа документа слишком длинный")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(IDocumentTypeRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Update {nameof(UpdateDocumentType)}");
            return new CommandResult(await repository.UpdateAsync(mapper.Map<DocumentType>(request.DocumentTypeDto)));
        }
    }
}