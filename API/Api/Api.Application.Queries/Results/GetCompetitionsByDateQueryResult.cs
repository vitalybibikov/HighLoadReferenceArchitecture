using System;
using System.Collections.Generic;

namespace Api.Application.Queries.Results
{
    public class GetCompetitionsByDateQueryResult
    {
        public List<GetCompetitionQueryResult> Competitions = new List<GetCompetitionQueryResult>();
    }
}
