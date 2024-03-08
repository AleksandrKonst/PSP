using Application.DTO.PassengerContextDTO;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.PassengerRepositories.Interfaces;
using MediatR;

namespace Application.MediatR.Queries.GenderTypeQueries;

public static class GetGenderTypes
{
    public record Query(int Index = 0, int Count = int.MaxValue) : IRequest<QueryResult>;

    public record QueryResult(IEnumerable<GenderTypeDTO> Result);
    
    public class Handler(IGenderTypeRepository repository, IMapper mapper) : IRequestHandler<Query, QueryResult>
    {
        public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
        {
            return new QueryResult(mapper.Map<IEnumerable<GenderTypeDTO>>(await repository.GetPartAsync(request.Index, request.Count)));
        }
    }
}