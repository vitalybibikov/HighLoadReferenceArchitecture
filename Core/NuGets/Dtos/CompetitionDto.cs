using System;
using System.Collections.Generic;
using System.Linq;
using Core.Core;
using Core.Core.Sports.Enums;

namespace Core.NuGets.Dtos
{
    public class CompetitionDto
    {
        public CompetitionDto(Competition competition)
        {
            Name = competition.Name;
            Place = competition.Place;
            StartDate = competition.StartDate;
            SportType = competition.SportType;
            Teams = competition.Teams.Select(x => new TeamDto()
            {
                Name = x.Name
            }).ToList();
            UniqueId = competition.UniqueId;
        }

        public CompetitionDto()
        {
        }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime StartDate { get; set; }

        public SportType SportType { get; set; }

        public List<TeamDto> Teams { get; set; } = new List<TeamDto>();

        public long UniqueId { get; set; }
    }
}
