using Application.DTO.PassengerContextDTO;
using AutoMapper;
using FluentValidation;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.PassengerQueries;

public static class GetPassengerByFio
{
    public record Query(string Name, string Surname,
        string? Patronymic, DateOnly Birthdate) : IRequest<QueryResult>;
    
    public record QueryResult(PassengerDTO Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Surname)
                .MaximumLength(40)
                .WithMessage("Длинная фамилия")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.Name)
                .MaximumLength(40)
                .WithMessage("Длинное имя")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.Patronymic)
                .MaximumLength(40)
                .WithMessage("Длинное отчество")
                .WithErrorCode("PPC-000403");
            
            RuleFor(x => x.Birthdate.Year)
                .LessThan(9999)
                .GreaterThan(1000)
                .WithMessage("Неверный формат года")
                .WithErrorCode("PPC-000403");
        }
    }
    
    public class Handler(IPassengerRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            var passenger = await repository.GetByFullNameAsync(request.Name, request.Surname, request.Patronymic, request.Birthdate);
            return new QueryResult(mapper.Map<PassengerDTO>(passenger));
        }
    }
}