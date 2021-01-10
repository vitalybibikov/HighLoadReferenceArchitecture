using System;
using System.Net.Http;
using System.Threading.Tasks;
using Core.NuGet.Shared.Extensions;
using Core.Sources.Contracts.Abstract;
using HtmlAgilityPack;

namespace Core.Sources.Implementations.LifeScores.Capabilities.Abstract
{
    public abstract class LifeScoresSportRetriever: SportsRetriever
    {
        protected LifeScoresSportRetriever(HttpClient client) 
            : base(client)
        {
        }

        private protected async Task<HtmlNodeCollection> GetNodes(Uri source)
        {
            var response = await Client.SendWithBrowserHeaders(source);
            var pageContents = await response.Content.ReadAsStringAsync();
            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            return pageDocument.DocumentNode.SelectNodes("(//div[contains(@data-type,'evt')])");
        }
    }
}
