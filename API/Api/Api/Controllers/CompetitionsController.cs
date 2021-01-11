using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Queries.Competitions;
using Api.Application.Queries.Results;
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
                Date = date //DateTime.Parse(date, Thread.CurrentThread.CurrentCulture)
            });

            return results.Competitions;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(DateTime.Now);
        }
    }
}
