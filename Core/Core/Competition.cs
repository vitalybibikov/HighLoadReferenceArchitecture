using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using Core.Core.Sports.Enums;
using Core.NuGet.Shared.Extensions;

namespace Core.Core
{
    public class Competition
    {
        public Competition(string name, string place, IEnumerable<Team> teams, DateTime startDate, SportType type)
        {
            Name = name;
            Place = place;
            StartDate = startDate;
            SportType = type;
            Teams = teams.ToImmutableArray();
        }

        public string Name { get; }

        public string Place { get; }

        public DateTime StartDate { get; }

        public SportType SportType { get; }

        public ImmutableArray<Team> Teams { get; }

        //for simplicity of a demo will be like so.
        // can be used as an Id to deduplicate messages in ServiceBus/RabbitMq/Kafka
        public long UniqueId
        {
            get
            {
                var teamsCode = Enumerable.Aggregate(Teams, 0, (current, team) => current ^ team.Name.GetStableHashCode());

                return teamsCode ^
                       Name.GetStableHashCode() ^
                       StartDate.ToString(CultureInfo.InvariantCulture).GetStableHashCode() ^
                       SportType.ToString().GetStableHashCode();
            }
        }
    }
}
