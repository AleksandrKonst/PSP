using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.TicketTypeCommands;

public static class CreateTicketType
{
    public record Command(TicketTypeDTO objDTO) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(ITicketTypeRepository repository)
        {
            RuleFor(x => x.objDTO.Code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор типа билета")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(ITicketTypeRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Create {nameof(CreateTicketType)}");
            return new CommandResult(await repository.AddAsync(mapper.Map<TicketType>(request.objDTO)));
        }
    }
}