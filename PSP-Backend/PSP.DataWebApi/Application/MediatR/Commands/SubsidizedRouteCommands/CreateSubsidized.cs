using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.SubsidizedRouteCommands;

public static class CreateSubsidized
{
    public record Command(SubsidizedRouteDTO objDTO) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(ISubsidizedRouteRepository repository)
        {
            RuleFor(x => x.objDTO.Id)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code) == false)
                .WithMessage("Имеется повторяющийся идентификатор маршрута")
                .WithErrorCode("PPC-000002");
        }
    }
    
    public class Handler(ISubsidizedRouteRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.AddAsync(mapper.Map<SubsidizedRoute>(request.objDTO)));
        }
    }
}