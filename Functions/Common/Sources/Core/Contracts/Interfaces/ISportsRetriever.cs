using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Sources.Core.Contracts.Interfaces
{
    public interface ISportsRetriever
    {
        /// <summary>
        /// Returns all matches by the url
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Task<List<Competition>> GetAllAsync();

        /// <summary>
        /// Returns only live match
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Task<CompetitionStats> GetLiveAsync(string content);
    }
}
