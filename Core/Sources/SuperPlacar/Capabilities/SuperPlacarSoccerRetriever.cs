using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApp1.Core;
using ConsoleApp1.Sources.SuperPlacar.Capabilities.Abstract;

namespace ConsoleApp1.Sources.SuperPlacar.Capabilities
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

        public override async Task<List<Competition>> GetAllAsync(Uri source)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Competition>> GetLiveAsync(Uri source)
        {
            throw new NotImplementedException();
        }

    }
}
