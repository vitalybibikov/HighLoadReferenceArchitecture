using System;
using Common.Sources.Core.Contracts.Interfaces;
using Common.Sources.Implementations.Builder.Abstract;
using Common.Sources.Implementations.LifeScores.Capabilities;
using NuGets.NuGets.Cache;
using NuGets.NuGets.Dtos.Enums;

namespace Common.Sources.Implementations.LifeScores.Builder
{
    public sealed class LiveScoresPathBuilder : PathBuilder, 
        ILiveScoresPathBuilder, 
        IOnDatePathBuilder
    {
        public override Uri SourceUri => new Uri("https://www.livescores.com/");

        //can use reflection on interfaces here, to ovoid OCP violation, but for the demo will suffice.
        public override IOnDatePathBuilder WithSport(SportType type)
        {
            switch (type)
            {
                case SportType.Soccer:
                    return WithSoccer();
                case SportType.Basketball:
                    return WithBasketball();
                case SportType.Tennis:
                    return WithTennis();
                case SportType.Hockey:
                    return WithHockey();
                default:
                    throw new NotSupportedException(nameof(type));
            }
        }

        public IOnDatePathBuilder WithSoccer()
        {
            CompetitionUri = new Uri("soccer/", UriKind.Relative);
            Type = SportType.Soccer;
            return this;
        }

        public IOnDatePathBuilder WithBasketball()
        {
            CompetitionUri = new Uri("tennis/", UriKind.Relative);
            Type = SportType.Basketball;

            return this;
        }

        public IOnDatePathBuilder WithTennis()
        {
            CompetitionUri = new Uri("basketball/", UriKind.Relative);
            Type = SportType.Tennis;

            return this;
        }

        public IOnDatePathBuilder WithHockey()
        {
            CompetitionUri = new Uri("hockey/", UriKind.Relative);
            Type = SportType.Hockey;

            return this;
        }

        public IPathBuilder OnDate(DateTime? time)
        {
            if (time.HasValue)
            {
                OnDateUri = new Uri($"{time.Value:yyyy-MM-dd/}", UriKind.Relative);
            }

            return this;
        }

        public IPathBuilder Live()
        {
            OnDateUri = new Uri($"live/", UriKind.Relative);
            return this;
        }

        public override ISportsRetriever Build()
        {
            var client = HttpClientCache.GetOrCreateClient(GetUri());

            return Type switch
            {
                SportType.Soccer => new LifeScoresSoccerRetriever(client),
                SportType.Basketball => new LifeScoresBasketballRetriever(client),
                SportType.Tennis => new LifeScoresTennisRetriever(client),
                SportType.Hockey => new LifeScoresHockeyRetriever(client),
                _ => throw new NotSupportedException(nameof(Type))
            };
        }
    }
}
