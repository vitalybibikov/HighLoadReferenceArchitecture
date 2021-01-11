using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Queries.Competitions;
using Api.Application.Queries.Results;
using Api.Infrastructure.Queries.Handlers.Base;
using Api.Infrastructure.Settings;
using Api.MongoDb.Dtos;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Infrastructure.Queries.Handlers
{
    public class GetCompetitionsByDateQueryHandler: BaseCompetitionsHandler
        , IRequestHandler<GetCompetitionsByDateQuery, GetCompetitionsByDateQueryResult>
    {
        public GetCompetitionsByDateQueryHandler(IMongoClient client, IOptions<CompetitionsStoreDbSettings> settings) 
            : base(client, settings)
        {
        }

        public async Task<GetCompetitionsByDateQueryResult> Handle(GetCompetitionsByDateQuery request,
            CancellationToken cancellationToken)
        {
            var items =  await Competitions
                .Find<CompetitionDto>(x => x.StartDate.Date == request.Date.Date)
                .ToListAsync(cancellationToken);

            var results = items
                .Select(x => new GetCompetitionQueryResult {Date = x.StartDate, Name = x.Name})
                .ToList();

            return new GetCompetitionsByDateQueryResult()
            {
                Competitions = results
            };
        }
    }
}
