using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.Core.Sports.Enums;
using Core.NuGet.Contracts;
using Core.Sources.Implementations.LifeScores;
using Core.Sources.Implementations.LifeScores.Builder;

namespace Core
{
    class Parser
    {
        static async Task Main(string[] args)
        {
            TimeSpan span = TimeSpan.Zero;
            
            for (int i = 0; i <= 99; i++)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                
                var pathBuilder = new LiveScoresPathBuilder();
                var sportsSource = new LifeScoresSource(pathBuilder);
                
                var crawler = sportsSource.GetRetriever(new SyncMessage()
                {
                    SportType = SportType.Soccer
                });
                var competitions = await crawler.GetAllAsync();

                stopWatch.Stop();
                span = stopWatch.Elapsed;
                Console.WriteLine(span);
            }

            Console.ReadKey();
        }
    }
}
