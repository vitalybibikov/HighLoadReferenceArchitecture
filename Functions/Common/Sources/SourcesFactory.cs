using Common.Sources.Core.Contracts.Interfaces;
using Common.Sources.Implementations.Builder.Abstract;
using Common.Sources.Implementations.LifeScores;
using NuGets.NuGets.Contracts.Enums;

namespace Common.Sources
{
    public class SourcesFactory
    {
        private readonly IPathBuilder builder;

        public SourcesFactory(IPathBuilder builder)
        {
            this.builder = builder;
        }

        //todo: returns source depending on the params, simplified to 1;
        public ISportsSource GetSource(SourceConnectorType connectorType) => new LifeScoresSource(builder);
    }
}
