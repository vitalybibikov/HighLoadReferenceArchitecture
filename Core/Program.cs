using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApp1.Builder.Concrete;
using ConsoleApp1.Core.Sports.Enums;
using ConsoleApp1.Sources.Interfaces;
using ConsoleApp1.Sources.LifeScoresSource;

namespace ConsoleApp1
{
    class Parser
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            TimeSpan r = TimeSpan.Zero;
            
            for (int i = 0; i <= 99; i++)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var liveScores = new LiveScoresPathBuilder(new Uri("https://www.livescores.com/"));

                var source = liveScores
                    .WithSoccer()
                    .Live()
                    .Build();

                ISportsSource sportsSource =  new LifeScoresSource(client);
                var crawler = sportsSource.GetCrawler(SportType.Soccer);
                var competitions = await crawler.GetAllAsync(source);

                stopWatch.Stop();
                r = stopWatch.Elapsed;
                Console.WriteLine(r);
            }

            Console.ReadKey();
        }
    }
}
