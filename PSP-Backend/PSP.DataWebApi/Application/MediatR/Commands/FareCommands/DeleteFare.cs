﻿using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.FareCommands;

public static class DeleteFare
{
    public record Command(string code) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IFareRepository repository)
        {
            RuleFor(x => x.code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор тарифа не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IFareRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Delete {nameof(DeleteFare)}");
            return new CommandResult(await repository.DeleteAsync(request.code));
        }
    }
}