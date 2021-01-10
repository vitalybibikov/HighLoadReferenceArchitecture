using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Core;
using Core.Sources.Contracts.Interfaces;

namespace Core.Sources.Contracts.Abstract
{
    public abstract class SportsRetriever: ISportsRetriever
    {
        private protected HttpClient Client { get; }

        protected SportsRetriever(HttpClient client)
        {
            Client = client;
        }

        public abstract Task<Competition> GetOneAsync(Uri source);

        public abstract Task<List<Competition>> GetAllAsync();

        public abstract Task<List<Competition>> GetLiveAsync();
    }
}
