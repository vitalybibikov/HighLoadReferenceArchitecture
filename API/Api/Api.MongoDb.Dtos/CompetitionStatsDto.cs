using System;
using Api.DomainModels;

namespace Api.MongoDb.Dtos
{
    public class CompetitionStatsDto
    {
        public CompetitionStatsDto()
        {
        }

        public CompetitionStatsDto(CompetitionStats stats)
        {
            if (stats == null)
            {
                throw new ArgumentNullException(nameof(stats));
            }

            Score = stats.Score;
        }

        public string Score { get; set; }
    }
}
