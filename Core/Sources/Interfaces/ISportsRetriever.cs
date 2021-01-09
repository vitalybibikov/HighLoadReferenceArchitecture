﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleApp1.Core;

namespace ConsoleApp1.Sources.Interfaces
{
    public interface ISportsRetriever
    {
        /// <summary>
        /// Returns exactly one Competition by the url provided
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Task<Competition> GetOneAsync(Uri source);

        /// <summary>
        /// Returns all matches by the url
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Task<List<Competition>> GetAllAsync(Uri source);

        /// <summary>
        /// Returns only live matches by visiting a specified page or parsing all the matches.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        Task<List<Competition>> GetLiveAsync(Uri source);
    }
}
