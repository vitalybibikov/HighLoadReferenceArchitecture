using System;

namespace ConsoleApp1.Builder
{
    public interface IOnDatePathBuilder : IPathBuilder
    {
        IPathBuilder OnDate(DateTime time);

        IPathBuilder Live();
    }
}
