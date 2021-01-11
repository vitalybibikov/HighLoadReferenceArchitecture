using System.Threading.Tasks;
using Api.DomainModels;
using Api.DomainModels.Repository;
using Api.Infrastructure.Settings;
using Api.MongoDb.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Insfrastructure.Repository
{
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly IMongoCollection<CompetitionDto> competitions;

        public CompetitionRepository(IMongoClient client, IOptions<CompetitionsStoreDbSettings> settings)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            competitions = database.GetCollection<CompetitionDto>(settings.Value.CollectionName);
        }

        public async Task CreateAsync(Competition competition)
        {
            var dto = new CompetitionDto(competition);
            await competitions.InsertOneAsync(dto);
        }

        public async Task UpdateAsync(Competition competition)
        {
            var dto = new CompetitionDto(competition);
            await competitions.ReplaceOneAsync(book => dto.Id == competition.Id, dto);
        }
    }
}
