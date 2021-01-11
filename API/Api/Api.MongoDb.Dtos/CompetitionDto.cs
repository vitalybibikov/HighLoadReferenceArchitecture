using System;
using System.Collections.Generic;
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

            Id = competition.Id!;
            Name = competition.Name;
            Place = competition.Place;
            StartDate = competition.StartDate;
            Teams = new List<Team>();
            SportType = competition.Type;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime StartDate { get; set; }

        public SportType SportType { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>(2);
    }
}
