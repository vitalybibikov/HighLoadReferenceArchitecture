using Core.Core.Sports.Enums;
using Core.Sources.Contracts.Interfaces;

namespace Core.Sources.Implementations.Builder.Abstract
{
    public interface IPathBuilder
    {
        public IOnDatePathBuilder WithSport(SportType type);

        ISportsRetriever Build();
    }
}
