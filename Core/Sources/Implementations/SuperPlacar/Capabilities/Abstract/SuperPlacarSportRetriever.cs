using System;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Sources.Contracts.Abstract;
using HtmlAgilityPack;

namespace Core.Sources.Implementations.SuperPlacar.Capabilities.Abstract
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
