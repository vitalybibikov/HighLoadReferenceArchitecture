using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Queries.Results.Sources;
using Api.Application.Queries.Sources;
using MediatR;
using NuGets.NuGets.Contracts.Enums;

namespace Api.Infrastructure.Queries.Handlers
{
    public class GetSourcesQueryHandler : IRequestHandler<GetSourcesQuery, GetSourcesQueryResult>
    {
        public GetSourcesQueryHandler()
        {
        }

        public async Task<GetSourcesQueryResult> Handle(GetSourcesQuery request,
            CancellationToken cancellationToken)
        {
            //todo: get them from the db or something.

            var source = new GetOneSourceQueryResult
            {
                ConnectorType = SourceConnectorType.LifeScores,
                Uri = new Uri("https://www.livescores.com/"),
                SourceType = SourceType.Html
            };

            var result = new GetSourcesQueryResult
            {
                Sources = new List<GetOneSourceQueryResult> {source}
            };

            return result;
        }
    }
}
