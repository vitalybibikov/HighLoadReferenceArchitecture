﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Queries.Competitions;
using Api.Application.Queries.Results;
using Api.Application.Queries.Results.Competitions;
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
                .Find<CompetitionDto>(x => x.CompetitionDate == request.Date)
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
