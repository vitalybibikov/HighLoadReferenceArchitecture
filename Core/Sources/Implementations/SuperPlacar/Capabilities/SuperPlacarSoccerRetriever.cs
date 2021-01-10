using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Core;
using Core.Sources.Implementations.SuperPlacar.Capabilities.Abstract;

namespace Core.Sources.Implementations.SuperPlacar.Capabilities
{
    public sealed class SuperPlacarSoccerRetriever : SuperPlacarSportRetriever
    {
        public SuperPlacarSoccerRetriever(HttpClient client) 
            : base(client)
        {
        }

        public override async Task<Competition> GetOneAsync(Uri source)
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
