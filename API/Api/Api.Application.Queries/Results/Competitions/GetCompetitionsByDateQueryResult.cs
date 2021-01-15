using System.Collections.Generic;

namespace Api.Application.Queries.Results.Competitions
{
    public class GetCompetitionsByDateQueryResult
    {
        public List<GetCompetitionQueryResult> Competitions = new List<GetCompetitionQueryResult>();
    }
}
