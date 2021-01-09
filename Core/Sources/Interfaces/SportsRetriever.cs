using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApp1.Core;

namespace ConsoleApp1.Sources.Interfaces
{
    public abstract class SportsRetriever: ISportsRetriever
    {
        private protected HttpClient Client { get; }

        protected SportsRetriever(HttpClient client)
        {
            Client = client;
        }

        public abstract Task<Competition> GetOneAsync(Uri source);

        public abstract Task<List<Competition>> GetAllAsync(Uri source);

        public abstract Task<List<Competition>> GetLiveAsync(Uri source);
    }
}
