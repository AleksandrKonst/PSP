using AutoMapper;
using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO.FlightContextDTO;
using PSP.Domain.Models;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.CouponEventCommands;

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
    
    public class Handler(ICouponEventRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<CouponEvent>(request.CouponEventDto)));
        }
    }
}