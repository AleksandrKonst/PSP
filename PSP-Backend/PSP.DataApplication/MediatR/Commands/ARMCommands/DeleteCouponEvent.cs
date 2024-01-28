using FluentValidation;
using MediatR;
using PSP.DataApplication.DTO.ArmContextDTO.Delete;
using PSP.Infrastructure.Repositories.FlightRepositories.Interfaces;

namespace PSP.DataApplication.MediatR.Commands.ARMCommands;

public static class DeleteCouponEvent
{
    public record Command(DeleteCouponEventRequestDTO DeleteCouponEvent) : IRequest<CommandResult>;
    
    public record CommandResult(int Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(ICouponEventRepository repository)
        {
            
        }
    }
    
    public class Handler(ICouponEventRepository repository) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.DeleteByTicketAsync(request.DeleteCouponEvent.OperationType, 
                request.DeleteCouponEvent.TicketType, 
                request.DeleteCouponEvent.TicketNumber, 
                DateTime.Parse(request.DeleteCouponEvent.OperationDatetime).ToUniversalTime()));
        }
    }
}