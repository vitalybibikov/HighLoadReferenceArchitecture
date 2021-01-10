using System;

namespace Core.Sources.Implementations.Builder.Abstract
{
    public interface IOnDatePathBuilder : IPathBuilder
    {
        IPathBuilder OnDate(DateTime? time);

        IPathBuilder Live();
    }
}
