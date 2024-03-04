using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.AirlineCommands;

public static class CreateAirline
{
    public record Command(AirlineDTO objDTO) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IAirlineRepository repository)
        {
            RuleFor(x => x.objDTO.IataCode)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор перевозчика")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(IAirlineRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Create {nameof(CreateAirline)}");
            return new CommandResult(await repository.AddAsync(mapper.Map<Airline>(request.objDTO)));
        }
    }
}