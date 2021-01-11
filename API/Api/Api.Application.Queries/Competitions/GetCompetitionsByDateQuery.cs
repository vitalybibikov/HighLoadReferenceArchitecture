using System;
using Api.Application.Queries.Results;
using MediatR;

namespace Api.Application.Queries.Competitions
{
    public class GetCompetitionsByDateQuery : IRequest<GetCompetitionsByDateQueryResult>
    {
        public DateTime Date { get; set; }
    }
}
