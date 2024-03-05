using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.AirportCommands;

public static class UpdateAirport
{
    public record Command(AirportDTO objDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IAirportRepository repository)
        {
            RuleFor(x => x.objDto.IataCode)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор аэропорта не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IAirportRepository repository, IMapper mapper, ILogger<CreateAirport.Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Update {nameof(UpdateAirport)}");
            return new CommandResult(await repository.UpdateAsync(mapper.Map<Airport>(request.objDto)));
        }
    }
}