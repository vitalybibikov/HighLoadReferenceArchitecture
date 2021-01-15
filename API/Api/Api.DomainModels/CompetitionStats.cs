using System;

namespace Api.DomainModels
{
    public class CompetitionStats
    {
        public CompetitionStats(string score)
        {
            Score = score;
        }

        public string Score { get; set; }
    }
}
