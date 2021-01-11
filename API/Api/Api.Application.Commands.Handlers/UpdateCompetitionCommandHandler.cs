using System.Threading;
using System.Threading.Tasks;
using Api.Application.Commands.Base;
using MediatR;

namespace Api.Application.Commands.Handlers
{
    public class UpdateCompetitionCommandHandler : IRequestHandler<UpdateCompetitionCommand, CommandResult>
    {
        public Task<CommandResult> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
