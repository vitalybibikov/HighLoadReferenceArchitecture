using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApp1.Core;
using ConsoleApp1.Sources.LifeScoresSource.Capabilities.Abstract;

namespace ConsoleApp1.Sources.LifeScoresSource.Capabilities
{
    public sealed class LifeScoresTennisRetriever : LifeScoresSportRetriever
    {
        public LifeScoresTennisRetriever(HttpClient client) 
            : base(client)
        {
        }

        public override Task<Competition> GetOneAsync(Uri source)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Competition>> GetAllAsync(Uri source)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Competition>> GetLiveAsync(Uri source)
        {
            throw new NotImplementedException();
        }
    }
}
