using System;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApp1.Sources.Interfaces;
using HtmlAgilityPack;

namespace ConsoleApp1.Sources.SuperPlacar.Capabilities.Abstract
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
