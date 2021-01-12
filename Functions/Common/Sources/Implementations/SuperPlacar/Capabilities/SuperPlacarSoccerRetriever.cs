using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Sources.Core;
using Common.Sources.Implementations.SuperPlacar.Capabilities.Abstract;

namespace Common.Sources.Implementations.SuperPlacar.Capabilities
{
    public sealed class SuperPlacarSoccerRetriever : SuperPlacarSportRetriever
    {
        public SuperPlacarSoccerRetriever(HttpClient client) 
            : base(client)
        {
        }

        public override Task<List<Competition>> GetAllByContent(string source)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Competition>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Competition>> GetLiveAsync()
        {
            throw new NotImplementedException();
        }

    }
}
