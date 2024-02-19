using Application.DTO.FlightContextDTO;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories.FlightRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Commands.SubsidizedRouteCommands;

public static class UpdateSubsidizedRoute
{
    public record Command(SubsidizedRouteDTO objDto) : IRequest<CommandResult>;
    
    public record CommandResult(bool Result);
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator(ISubsidizedRouteRepository repository)
        {
            RuleFor(x => x.objDto.Id)
                .MustAsync(async (code, cancellationToken) => await repository.CheckByCodeAsync(code))
                .WithMessage("Идентификатор маршрута не существует")
                .WithErrorCode("PPC-000001");
        }
    }
    
    public class Handler(ISubsidizedRouteRepository repository, IMapper mapper) : IRequestHandler<Command, CommandResult>
    {
        public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
        {
            return new CommandResult(await repository.UpdateAsync(mapper.Map<SubsidizedRoute>(request.objDto)));
        }
    }
}