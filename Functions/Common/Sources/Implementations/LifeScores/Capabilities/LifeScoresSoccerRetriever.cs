using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common.Sources.Core;
using Common.Sources.Implementations.LifeScores.Capabilities.Abstract;
using HtmlAgilityPack;
using NuGets.NuGets.Dtos.Enums;

namespace Common.Sources.Implementations.LifeScores.Capabilities
{
    public sealed class LifeScoresSoccerRetriever : LifeScoresSportRetriever
    {
        public LifeScoresSoccerRetriever(HttpClient client) 
            : base(client)
        {
        }

        public override async Task<Competition> GetOneAsync(Uri source)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Competition>> GetAllAsync()
        {
            var nodes = await GetNodes(Client.BaseAddress);
            return nodes.Select(ParseNode).ToList();
        }

        private Competition ParseNode(HtmlNode node)
        {
            Competition competition = null;
            var time = node.Attributes["data-esd"].Value;
            var dateTime = DateTime.ParseExact(time, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var team1Name = node.SelectSingleNode("div[contains(@class,'ply tright name')]").InnerText.Trim();
            var team2Name = node.SelectSingleNode("div[contains(@class,'ply name')]").InnerText.Trim();

            var linkParts = node
                .SelectSingleNode("div[contains(@class,'sco')]//a")
                ?.Attributes["href"]
                ?.Value
                ?.Trim('/')
                ?.Split('/');

            var team1 = new Team(team1Name);
            var team2 = new Team(team2Name);

            if (linkParts != null && linkParts.Length > 3)
            {
                var place = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(linkParts[1]).Trim();
                var competitionName = linkParts[2].Replace('-', ' ');
                competitionName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(competitionName).Trim();

                competition = new Competition(competitionName, place, new[] {team1, team2}, dateTime, SportType.Soccer);
            }
            else
            {
                competition = new Competition(String.Empty, String.Empty, new[] { team1, team2 }, dateTime, SportType.Soccer);
            }

            return competition;
        }

        public override async Task<List<Competition>> GetLiveAsync()
        {
            throw new NotImplementedException();
        }

    }
}
