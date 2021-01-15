using System.Linq;
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

        public async Task UpsertAsync(Competition competition)
        {
            var dto = new CompetitionDto(competition);

            await competitions.ReplaceOneAsync(
                filter => filter.Id.Equals(competition.UniqueId), dto,
                new ReplaceOptions { IsUpsert = true });
        }

        public Task<bool> ExistsAsync(Competition competition)
        {
            var dto = new CompetitionDto(competition);
            long count = competitions.CountDocuments(filter => filter.Id.Equals(dto.Id));

            return Task.FromResult(count > 0);
        }

        public async Task<Competition> GetAsync(string id)
        {
            Competition competition = null;
            var competitionDto = await competitions.Find(filter => filter.Id.Equals(id)).SingleOrDefaultAsync();

            if (competitionDto != null)
            {
                //here we can use double dispatch to avoid the switch case, but not for POC.
                var teams = competitionDto.Teams
                    .Select(x => new Team(x.Name))
                    .ToList();

                competition = new Soccer(
                    competitionDto.Name,
                    competitionDto.Place,
                    teams,
                    competitionDto.StartDate,
                    competitionDto.Id,
                    competitionDto.LiveUri);
            }

            return competition;
        }
    }
}
