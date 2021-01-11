using System;
using Common.Sources.Core.Contracts.Interfaces;
using NuGets.NuGets.Dtos.Enums;

namespace Common.Sources.Implementations.Builder.Abstract
{
    public abstract class PathBuilder : IPathBuilder
    {
        public abstract Uri SourceUri { get; }

        private protected Uri CompetitionUri { get; set; }

        private protected SportType? Type { get; set; }

        private protected Uri OnDateUri { get; set; }

        public abstract IOnDatePathBuilder WithSport(SportType type);

        public Uri GetUri()
        {
            Uri result = SourceUri;

            if (CompetitionUri != null)
            {
                result = new Uri(SourceUri, CompetitionUri);
            }

            if (OnDateUri != null)
            {
                result = new Uri(result, OnDateUri);
            }

            return result;
        }

        public abstract ISportsRetriever Build();
    }
}
