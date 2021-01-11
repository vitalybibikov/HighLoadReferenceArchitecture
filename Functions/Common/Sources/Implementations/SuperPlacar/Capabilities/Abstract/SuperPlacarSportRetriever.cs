using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Sources.Core.Contracts.Abstract;
using HtmlAgilityPack;

namespace Common.Sources.Implementations.SuperPlacar.Capabilities.Abstract
{
    public abstract class SuperPlacarSportRetriever: SportsRetriever
    {
        protected SuperPlacarSportRetriever(HttpClient client) 
            : base(client)
        {
        }

        private protected async Task<HtmlNodeCollection> GetNodes(Uri source)
        {
            throw new NotImplementedException();
        }
    }
}
