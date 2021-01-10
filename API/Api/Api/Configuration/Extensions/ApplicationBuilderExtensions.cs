using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Api.Configuration.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static void Use365ExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(options =>
            {
                options.Run(
                    async context =>
                    {
                        var feature = context.Features.Get<IExceptionHandlerFeature>();
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        if (feature != null)
                        {
                            var err = $"Error: {feature.Error.Message}{feature.Error.StackTrace}";
                            Log.Error(feature.Error, "Server Error", feature);
                            await context.Response.WriteAsync(err);
                        }
                    });
            });
        }
    }
}
