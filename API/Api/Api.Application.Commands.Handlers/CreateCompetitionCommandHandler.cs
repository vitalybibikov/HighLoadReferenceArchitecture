using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Commands.Base;
using Api.Application.Notifications;
using Api.Core.Shared.Enums;
using Api.DomainModels;
using Api.DomainModels.Repository;
using MediatR;
using NuGets.NuGets.Contracts.Enums;
using SportType = NuGets.NuGets.Dtos.Enums.SportType;

namespace Api.Application.Commands.Handlers
{
    public class CreateCompetitionCommandHandler : IRequestHandler<CreateCompetitionCommand, CommandResult>
    {
        private readonly ICompetitionRepository repository;
        private readonly IMediator mediator;

        public CreateCompetitionCommandHandler(ICompetitionRepository repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task<CommandResult> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
        {
            //Each new message is found based on it's uniquenes id, that we calculate out of some fields, that considered to be unique.
            //Currently, it's always being replaced with it's new version.

            var teams = request.Teams.Select(x => new Team(x.Name)).ToList();
            var competition = new Soccer(
                request.Name, 
                request.Place, 
                teams, 
                request.StartDate, 
                request.UniqueId.ToString(),
                request.LiveUri);

            await repository.UpsertAsync(competition);

            //todo: in real example, should be in transactional manner;
            //all settings for the demo to be working, specified for the simplicity here
            //we should check whether it existed in the db or
            //todo: check whether it existed in the db.
            await mediator.Publish(
                new SendLiveSourceNotification
                {
                    ConnectorType = SourceConnectorType.LifeScores,
                    SourceType = SourceType.Api,
                    Uri = request.LiveUri,
                    When = DateTime.Now.AddMinutes(1), //
                    PollingIntervalInSec = 10,
                    StartTime = DateTime.Now.AddMinutes(1),
                    FinishTime = DateTime.Now.AddMinutes(2),
                    SportType = SportType.Soccer
                },
                cancellationToken);

            return new CommandResult
            {
                Success = true
            };
        }
    }
}
