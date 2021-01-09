using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1.Core;
using ConsoleApp1.Core.Sports.Enums;
using ConsoleApp1.Sources.LifeScoresSource.Capabilities.Abstract;

namespace ConsoleApp1.Sources.LifeScoresSource.Capabilities
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

        public override async Task<List<Competition>> GetAllAsync(Uri source)
        {
            var nodes = await GetNodes(source);
            var competitions = new List<Competition>();

            foreach (var node in nodes)
            {
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

                if (linkParts != null && linkParts.Length > 3)
                {
                    var place = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(linkParts[1]).Trim();
                    var competitionName = linkParts[2].Replace('-', ' ');
                    competitionName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(competitionName).Trim();

                    var team1 = new Team(team1Name);
                    var team2 = new Team(team2Name);

                    var competition = new Competition(competitionName, place, new[] { team1, team2 }, dateTime, SportType.Soccer);
                    competitions.Add(competition);
                }
            }

            return competitions;
        }

        public override async Task<List<Competition>> GetLiveAsync(Uri source)
        {
            throw new NotImplementedException();
        }

    }
}
