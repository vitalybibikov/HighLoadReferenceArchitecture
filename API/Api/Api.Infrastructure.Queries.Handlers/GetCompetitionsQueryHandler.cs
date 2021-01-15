using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Queries.Competitions;
using Api.Application.Queries.Results.Competitions;
using Api.Infrastructure.Queries.Handlers.Base;
using Api.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Infrastructure.Queries.Handlers
{
    public class GetCompetitionsQueryHandler : BaseCompetitionsHandler
        , IRequestHandler<GetCompetitionsQuery, GetCompetitionsByDateQueryResult>
    {
        public GetCompetitionsQueryHandler(IMongoClient client, IOptions<CompetitionsStoreDbSettings> settings) 
            : base(client, settings)
        {
        }

        public async Task<GetCompetitionsByDateQueryResult> Handle(GetCompetitionsQuery request,
            CancellationToken cancellationToken)
        {
            //just demonstrates all from the collection
            var items = await Competitions.Find(_ => true)
                .ToListAsync(cancellationToken);

            var results = items
                .Select(x => new GetCompetitionQueryResult
                {
                    StartDate = x.StartDate,
                    Place = x.Place,
                    LiveUri = x.LiveUri,
                    SportType = x.SportType,
                    CompetitionDate = x.CompetitionDate,
                    Score = x.Stats?.Score,
                    Team1 = x.Teams?[0].Name,
                    Team2 = x.Teams?[1].Name,
                    Name = x.Name
                })
                .ToList();

            return new GetCompetitionsByDateQueryResult
            {
                Competitions = results
            };
        }
    }
}
