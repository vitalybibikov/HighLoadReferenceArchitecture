using System;
using System.Collections.Generic;
using System.Net.Http;
using ConsoleApp1.Core.Sports.Enums;
using ConsoleApp1.Sources.Interfaces;
using ConsoleApp1.Sources.LifeScoresSource.Capabilities;

namespace ConsoleApp1.Sources.SuperPlacar
{
    public class SuperPlacarSource: ISportsSource
    {
        private readonly HttpClient client;

        public SuperPlacarSource(HttpClient client)
        {
            this.client = client;
        }

        public SourceType SourceType => SourceType.Html;

        public List<SportType> GetCapabilities()
        {
            return new List<SportType>
            {
                SportType.Soccer
            };
        }

        //can use reflection here to eliminate violation of open/closed or interfaces
        public ISportsRetriever GetCrawler(SportType type) =>
            type switch
            {
                SportType.Soccer => new LifeScoresSoccerRetriever(client),

                _ => throw new NotSupportedException(nameof(type))
            };
    }
}
