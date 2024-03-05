using Application.DTO.PassengerContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.PassengerCommands;

public static class UpdatePassenger
{
    public record Command(PassengerDTO PassengerDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IPassengerRepository repository)
        {
            RuleFor(x => x.PassengerDto.Id)
                .MustAsync(async (id, cancellationToken) => await repository.CheckByIdAsync(id))
                .WithMessage("Идентификатор пассажира не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IPassengerRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Update {nameof(UpdatePassenger)}");
            return new CommandResult(await repository.UpdateAsync(mapper.Map<Passenger>(request.PassengerDto)));
        }
    }
}