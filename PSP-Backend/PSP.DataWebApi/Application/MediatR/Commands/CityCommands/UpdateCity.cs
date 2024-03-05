using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.CityCommands;

public static class UpdateCity
{
    public record Command(CityDTO objDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(ICityRepository repository)
        {
            RuleFor(x => x.objDto.IataCode)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор города не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(ICityRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Update {nameof(UpdateCity)}");
            return new CommandResult(await repository.UpdateAsync(mapper.Map<City>(request.objDto)));
        }
    }
}