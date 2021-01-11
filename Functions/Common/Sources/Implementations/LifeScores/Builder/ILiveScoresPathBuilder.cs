using Common.Sources.Implementations.Builder.Abstract;

namespace Common.Sources.Implementations.LifeScores.Builder
{
    public interface ILiveScoresPathBuilder : IPathBuilder
    {
        public IOnDatePathBuilder WithSoccer();

        public IOnDatePathBuilder WithBasketball();

        public IOnDatePathBuilder WithTennis();

        public IOnDatePathBuilder WithHockey();
    }
}
