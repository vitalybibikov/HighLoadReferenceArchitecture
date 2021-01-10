using System;
using System.Collections.Generic;
using Core.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompetitionsController : ControllerBase
    {
        private readonly ILogger<CompetitionsController> logger;

        public CompetitionsController(ILogger<CompetitionsController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<Competition> Get()
        {
            throw new NotImplementedException();
        }
    }
}
