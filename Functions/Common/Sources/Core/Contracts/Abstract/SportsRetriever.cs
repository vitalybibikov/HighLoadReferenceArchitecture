using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Sources.Core.Contracts.Interfaces;

namespace Common.Sources.Core.Contracts.Abstract
{
    public abstract class SportsRetriever: ISportsRetriever
    {
        private protected HttpClient Client { get; }

        protected SportsRetriever(HttpClient client)
        {
            Client = client;
        }

        public abstract Task<List<Competition>> GetAllByContent(string source);

        public abstract Task<List<Competition>> GetAllAsync();

        public abstract Task<List<Competition>> GetLiveAsync();
    }
}
