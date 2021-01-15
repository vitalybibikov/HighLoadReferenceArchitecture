using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Queries.Competitions;
using Api.Application.Queries.Results;
using Api.Application.Queries.Results.Competitions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompetitionsController : ControllerBase
    {
        private readonly ILogger<CompetitionsController> logger;
        private readonly IMediator mediator;

        public CompetitionsController(ILogger<CompetitionsController> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("{date}")]
        public async Task<IEnumerable<GetCompetitionQueryResult>> Get(DateTime date)
        {
            var results = await mediator.Send(new GetCompetitionsByDateQuery
            {
                Date = date
            });

            return results.Competitions;
        }

        [HttpGet]
        public async Task<IEnumerable<GetCompetitionQueryResult>> Get()
        {
            var results = await mediator.Send(new GetCompetitionsQuery
            {
            });

            return results.Competitions;
        }
    }
}
