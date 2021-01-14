using Api.Application.Queries.Results;
using Api.Application.Queries.Results.Sources;
using MediatR;

namespace Api.Application.Queries.Sources
{
    public class GetSourcesQuery : IRequest<GetSourcesQueryResult>
    {
    }
}
