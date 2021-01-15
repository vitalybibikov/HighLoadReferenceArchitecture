using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Sources.Core;
using Common.Sources.Implementations.LifeScores.Capabilities.Abstract;

namespace Common.Sources.Implementations.LifeScores.Capabilities
{
    public sealed class LifeScoresTennisRetriever : LifeScoresSportRetriever
    {
        public LifeScoresTennisRetriever(HttpClient client) 
            : base(client)
        {
        }

        public override Task<List<Competition>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<CompetitionStats> GetLiveAsync(string content)
        {
            throw new NotImplementedException();
        }
    }
}
