using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Application.Commands.Base;
using Api.DomainModels;
using Api.DomainModels.Repository;
using MediatR;

namespace Api.Application.Commands.Handlers
{
    public class CreateCompetitionStatsCommandHandler : IRequestHandler<CreateCompetitionStatsCommand, CommandResult>
    {
        private readonly ICompetitionRepository repository;

        public CreateCompetitionStatsCommandHandler(ICompetitionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(CreateCompetitionStatsCommand request, CancellationToken cancellationToken)
        {
            if (!String.IsNullOrEmpty(request.Score))

            {   //todo: should be find and update operation later on.

                var competition = await repository.GetAsync(request.CompetitionId);

                if (competition != null)
                {
                    competition.AddStats(new CompetitionStats(request.Score));
                    await repository.UpsertAsync(competition);
                }
            }

            return new CommandResult
            {
                Success = true
            };
        }
    }
}
