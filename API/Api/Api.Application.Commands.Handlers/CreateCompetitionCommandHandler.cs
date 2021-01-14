using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Commands.Base;
using Api.DomainModels;
using Api.DomainModels.Repository;
using MediatR;

namespace Api.Application.Commands.Handlers
{
    public class CreateCompetitionCommandHandler : IRequestHandler<CreateCompetitionCommand, CommandResult>
    {
        private readonly ICompetitionRepository repository;

        public CreateCompetitionCommandHandler(ICompetitionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
        {
            //Each new message is found based on it's uniquenes id, that we calculate out of some fields, that considered to be unique.
            //Currently, it's always being replaced with it's new version.
            var teams = request.Teams.Select(x => new Team(x.Name)).ToList();
            var competition = new Soccer(request.Name, request.Place, teams, request.StartDate, request.UniqueId.ToString());

            await repository.UpsertAsync(competition);

            return new CommandResult
            {
                Success = true
            };
        }
    }
}
