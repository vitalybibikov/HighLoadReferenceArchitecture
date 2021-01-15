using Api.Application.Queries.Results.Competitions;
using MediatR;

namespace Api.Application.Queries.Competitions
{
    public class GetCompetitionsQuery : IRequest<GetCompetitionsByDateQueryResult>
    {
    }
}
