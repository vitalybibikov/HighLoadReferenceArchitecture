using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Sources.Core.Contracts.Abstract;
using HtmlAgilityPack;
using NuGets.NuGets.Extensions;

namespace Common.Sources.Implementations.LifeScores.Capabilities.Abstract
{
    public abstract class LifeScoresSportRetriever: SportsRetriever
    {
        protected LifeScoresSportRetriever(HttpClient client) 
            : base(client)
        {
        }

        private protected async Task<string> LoadContentAsync(Uri source)
        {
            var response = await Client.SendWithBrowserHeaders(source);
            var pageContents = await response.Content.ReadAsStringAsync();
            return pageContents;
        }

        private protected async Task<HtmlNodeCollection> GetNodes(Uri source)
        {
            var pageContents = await LoadContentAsync(source);
            return GetNodes(pageContents);
        }

        private protected HtmlNodeCollection GetNodes(string content)
        {
            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(content);
            return pageDocument.DocumentNode.SelectNodes("(//div[contains(@data-type,'evt')])");
        }
    }
}
