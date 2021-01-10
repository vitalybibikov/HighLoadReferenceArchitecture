using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Api.Configuration
{
    public static class EnvConfiguration
    {
        public static IConfigurationRoot ConfigureEnvironment(WebHostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            var env = hostingContext?.HostingEnvironment;
            var configuration = config
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env?.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }
    }
}
