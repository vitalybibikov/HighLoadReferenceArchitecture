using Common.Sources.Core.Contracts.Interfaces;
using NuGets.NuGets.Dtos.Enums;

namespace Common.Sources.Implementations.Builder.Abstract
{
    public interface IPathBuilder
    {
        public IOnDatePathBuilder WithSport(SportType type);

        ISportsRetriever Build();
    }
}
