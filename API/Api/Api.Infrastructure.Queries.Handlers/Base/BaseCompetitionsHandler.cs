using System;
using System.Collections.Generic;
using System.Text;
using Api.Infrastructure.Settings;
using Api.MongoDb.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Infrastructure.Queries.Handlers.Base
{
    public abstract class BaseCompetitionsHandler
    {
        public IMongoCollection<CompetitionDto> Competitions { get; }

        protected BaseCompetitionsHandler(IMongoClient client, IOptions<CompetitionsStoreDbSettings> settings)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            Competitions = database.GetCollection<CompetitionDto>(settings.Value.CollectionName);
        }
    }
}
