using System;
using Api.Core.Shared.Enums;

namespace Api.Application.Queries.Results.Competitions
{
    public class GetCompetitionQueryResult
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime StartDate { get; set; }

        public Uri LiveUri { get; set; }
        public DateTime CompetitionDate { get; set; }

        public SportType SportType { get; set; }

        public string? Score { get; set; }

        public string? Team1 { get; set; }

        public string? Team2 { get; set; }
    }
}
