using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Core;
using Core.Sources.Implementations.LifeScores.Capabilities.Abstract;

namespace Core.Sources.Implementations.LifeScores.Capabilities
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

        public override Task<List<Competition>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<List<Competition>> GetLiveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
