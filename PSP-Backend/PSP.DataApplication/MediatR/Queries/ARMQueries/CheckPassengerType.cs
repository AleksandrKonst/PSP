using FluentValidation;
using MediatR;

namespace PSP.DataApplication.MediatR.Queries.ARMQueries;

public static class CheckPassengerType
{
    public record Query(string Name, string Surname, string? Patronymic, DateOnly Birthdate) : IRequest<QueryResult>;
    
    public record QueryResult(bool Result);
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            //Заглушка (будет валидация)
        }
    }
    
    public class Handler() : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            //Заглушка (Проверяем тип во внешнем сервисе)
            return new QueryResult(true);
        }
    }
}