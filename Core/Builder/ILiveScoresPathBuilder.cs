using System;

namespace ConsoleApp1.Builder
{
    public interface ILiveScoresPathBuilder : IPathBuilder
    {
        public IOnDatePathBuilder WithSoccer();

        public IOnDatePathBuilder WithBasketball();

        public IOnDatePathBuilder WithTennis();

        public IOnDatePathBuilder WithHockey();
    }
}
