using System;
using System.Collections.Generic;
using Api.Application.Commands.Base;
using MediatR;

namespace Api.Application.Commands
{
    public class CreateTeamCommand : IRequest<CommandResult>
    {
        public string Name { get; set; }
    }
}
