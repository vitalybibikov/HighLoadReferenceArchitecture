using System;
using System.Collections.Generic;
using System.Linq;
using Api.Core.Shared.Enums;

namespace Api.DomainModels
{
    public abstract class Competition
    {
        protected Competition(string name, string place, ICollection<Team> teams, DateTime startDate, SportType type, string uniqueId, Uri liveUri)
        {
            UniqueId = uniqueId;
            Name = name;
            Place = place;
            StartDate = startDate;
            Teams = teams.ToList();
            Type = type;

            //todo: uris will be a set of source with a relation to type.
            LiveUri = liveUri;
        }

        public string Name { get; }

        public string Place { get; }

        public SportType Type { get; }

        public DateTime StartDate { get; }

        public string UniqueId { get; }

        public DateTime CompetitionDate => StartDate.Date;

        public List<Team> Teams { get; }

        public Uri LiveUri { get; set; }

        public CompetitionStats Stats { get; set; }

        public abstract void FinishGame(DateTime endDateTime);

        public void AddStats(CompetitionStats stats)
        {
            //just replacing them for now.
            Stats = stats;
        }
    }
}
