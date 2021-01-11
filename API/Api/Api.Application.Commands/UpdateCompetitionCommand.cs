using System;
using System.Collections.Generic;
using System.Text;
using Api.Application.Commands.Base;
using Api.Core.Shared.Enums;
using MediatR;

namespace Api.Application.Commands
{
    public class UpdateCompetitionCommand : IRequest<CommandResult>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime StartDate { get; set; }

        public SportType SportType { get; set; }

        public List<CreateTeamCommand> Teams { get; set; } = new List<CreateTeamCommand>();

        public long UniqueId { get; set; }
    }
}
