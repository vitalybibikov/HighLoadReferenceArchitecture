using System;
using System.Collections.Generic;
using NuGets.NuGets.Dtos.Enums;

namespace NuGets.NuGets.Dtos
{
    public class CompetitionMessage
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime StartDate { get; set; }

        public SportType SportType { get; set; }

        public List<TeamMessage> Teams { get; set; } = new List<TeamMessage>();

        public long UniqueId { get; set; }

        public Uri LiveUri { get; set; }
    }
}
