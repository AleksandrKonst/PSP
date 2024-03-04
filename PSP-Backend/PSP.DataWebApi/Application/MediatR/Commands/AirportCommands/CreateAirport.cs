using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.AirportCommands;

public static class CreateAirport
{
    public record Command(AirportDTO objDTO) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IAirportRepository repository)
        {
            RuleFor(x => x.objDTO.IataCode)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор аэропорта")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(IAirportRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Create {nameof(CreateAirport)}");
            return new CommandResult(await repository.AddAsync(mapper.Map<Airport>(request.objDTO)));
        }
    }
}