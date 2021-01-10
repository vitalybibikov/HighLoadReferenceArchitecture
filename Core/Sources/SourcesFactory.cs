using Core.Sources.Contracts.Enums;
using Core.Sources.Contracts.Interfaces;
using Core.Sources.Implementations.Builder.Abstract;
using Core.Sources.Implementations.LifeScores;

namespace Core.Sources
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
