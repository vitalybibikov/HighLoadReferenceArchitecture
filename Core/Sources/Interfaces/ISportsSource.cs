using System.Collections.Generic;
using ConsoleApp1.Core.Sports.Enums;

namespace ConsoleApp1.Sources.Interfaces
{
    public interface ISportsSource
    {
        public SourceType SourceType { get; }

        public List<SportType> GetCapabilities();

        public ISportsRetriever GetCrawler(SportType type);
    }
}
