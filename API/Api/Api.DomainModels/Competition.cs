using System;
using System.Collections.Generic;
using System.Linq;
using Api.Core.Shared.Enums;

namespace Api.DomainModels
{
    public abstract class Competition
    {
        protected Competition(string name, string place, ICollection<Team> teams, DateTime startDate, SportType type)
        {
            Name = name;
            Place = place;
            StartDate = startDate;
            Teams = teams.ToList();
            Type = type;
        }

        protected Competition(string id, string name, string place, ICollection<Team> teams, DateTime startDate, SportType type)
            :this(name, place, teams, startDate, type)
        {
            Id = id;
        }

        public string? Id { get; }

        public string Name { get; }

        public string Place { get; }

        public SportType Type { get; }

        public DateTime StartDate { get; }

        public DateTime CompetitionDate => StartDate.Date;

        public List<Team> Teams { get; }

        public abstract void FinishGame(DateTime endDateTime);
    }
}
