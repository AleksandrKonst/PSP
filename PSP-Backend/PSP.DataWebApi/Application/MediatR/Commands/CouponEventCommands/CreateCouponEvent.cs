using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Commands.CouponEventCommands;

public static class CreateCouponEvent
{
    public record Command(CouponEventDTO CouponEventDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(ICouponEventRepository repository)
        {
            RuleFor(x => x.CouponEventDto.Id)
                .MustAsync(async (id, cancellationToken) => await repository.CheckByIdAsync(id) == false)
                .WithMessage("Имеется повторяющийся идентификатор события с билетом")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(ICouponEventRepository repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Create {nameof(CreateCouponEvent)}");
            return new CommandResult(await repository.AddAsync(mapper.Map<CouponEvent>(request.CouponEventDto)));
        }
    }
}