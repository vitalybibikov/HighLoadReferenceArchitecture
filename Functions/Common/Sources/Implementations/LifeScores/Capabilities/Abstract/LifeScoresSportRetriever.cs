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

        private protected async Task<HtmlNode> GetNodesAsync(Uri source)
        {
            var pageContents = await LoadContentAsync(source);
            return GetContent(pageContents);
        }

        private protected async Task<HtmlNode> GetNodesAsync(string content)
        {
            return await Task.FromResult(GetContent(content));
        }

        private HtmlNode GetContent(string pageContents)
        {
            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);
            return pageDocument.DocumentNode;
        }

        private async Task<string> LoadContentAsync(Uri source)
        {
            var response = await Client.SendWithBrowserHeaders(source);
            var pageContents = await response.Content.ReadAsStringAsync();
            return pageContents;
        }
    }
}
