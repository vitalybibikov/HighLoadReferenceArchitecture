using System;
using System.Collections.Generic;
using Api.Application.Commands.Base;
using Api.Core.Shared.Enums;
using MediatR;

namespace Api.Application.Commands
{
    public class CreateCompetitionCommand : IRequest<CommandResult>
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime StartDate { get; set; }

        public SportType SportType { get; set; }

        public IEnumerable<CreateTeamCommand> Teams { get; set; } = new List<CreateTeamCommand>();

        public long UniqueId { get; set; }

        public Uri LiveUri { get; set; }
    }
}
