﻿using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.QuotaCategoryCommands;

public static class DeleteQuotaCategory
{
    public record Command(string code) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(IQuotaCategoryRepository repository)
        {
            RuleFor(x => x.code)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор перевозчика не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(IQuotaCategoryRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Delete {nameof(DeleteQuotaCategory)}");
            return new CommandResult(await repository.DeleteAsync(request.code));
        }
    }
}