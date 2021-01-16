using System;
using System.Collections.Generic;
using System.Linq;
using Api.Core.Shared.Enums;
using Api.DomainModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.MongoDb.Dtos
{
    public class CompetitionDto
    {
        public CompetitionDto()
        {
        }

        public CompetitionDto(Competition competition)
        {
            if (competition == null)
            {
                throw new ArgumentNullException(nameof(competition));
            }

            //todo: algorithm should be changed to be really unique
            Id = competition.UniqueId;

            Name = competition.Name;
            Place = competition.Place;
            StartDate = competition.StartDate;
            Teams = competition.Teams.Select(x => new TeamDto { Name = x.Name}).ToList();
            SportType = competition.Type;
            CompetitionDate = competition.CompetitionDate;
            Stats = competition.Stats;
            LiveUri = competition.LiveUri;
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CompetitionDate { get; set; }

        public SportType SportType { get; set; }

        public CompetitionStats Stats { get; set; }

        public Uri LiveUri { get; set; }

        public List<TeamDto> Teams { get; set; } = new List<TeamDto>(2);
    }
}
