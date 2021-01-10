using Core.Sources.Implementations.Builder.Abstract;

namespace Core.Sources.Implementations.LifeScores.Builder
{
    public interface ILiveScoresPathBuilder : IPathBuilder
    {
        public IOnDatePathBuilder WithSoccer();

        public IOnDatePathBuilder WithBasketball();

        public IOnDatePathBuilder WithTennis();

        public IOnDatePathBuilder WithHockey();
    }
}
