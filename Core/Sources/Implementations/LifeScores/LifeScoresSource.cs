using System;
using System.Collections.Generic;
using Core.Core.Sports.Enums;
using Core.NuGet.Contracts;
using Core.Sources.Contracts.Enums;
using Core.Sources.Contracts.Interfaces;
using Core.Sources.Implementations.Builder.Abstract;

namespace Core.Sources.Implementations.LifeScores
{
    public class LifeScoresSource: ISportsSource
    {
        private readonly IPathBuilder builder;

        public LifeScoresSource(IPathBuilder builder)
        {
            this.builder = builder;
        }

        public SourceConnectorType Type => SourceConnectorType.LifeScores;

        public SourceType SourceType => SourceType.Html;

        public List<SportType> GetCapabilities()
        {
            return new List<SportType>
            {
                SportType.Basketball,
                SportType.Hockey,
                SportType.Soccer,
                SportType.Tennis
            };
        }

        //can use reflection here to eliminate violation of open/closed or interfaces
        public ISportsRetriever GetRetriever(SyncMessage message)
        {
            if (!GetCapabilities().Contains(message.SportType))
            {
                throw new NotSupportedException(nameof(message.SportType));
            }

            var liveScores = builder
                .WithSport(message.SportType)
                .OnDate(message.When)
                .Build();

            return liveScores;
        }
    }
}
