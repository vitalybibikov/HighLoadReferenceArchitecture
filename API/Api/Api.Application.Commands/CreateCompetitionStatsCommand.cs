using System;
using System.Collections.Generic;
using Api.Application.Commands.Base;
using Api.Core.Shared.Enums;
using MediatR;

namespace Api.Application.Commands
{
    public class CreateCompetitionStatsCommand : IRequest<CommandResult>
    {
        public string Score { get; set; }

        public string CompetitionId { get; set; }
    }
}
