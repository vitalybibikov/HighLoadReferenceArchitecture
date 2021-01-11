using System;
using System.Collections.Generic;
using Api.DomainModels.Repository;
using Api.Insfrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Api.Configuration.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection collection)
        {
            collection.AddTransient<ICompetitionRepository, CompetitionRepository>();
        }
    }
}
