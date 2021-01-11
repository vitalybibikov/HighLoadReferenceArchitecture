using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NuGets.NuGets.Extensions
{
    public static class HttpClientExtensions
    {
        public  static async Task<HttpResponseMessage> SendWithBrowserHeaders(this HttpClient client, Uri source)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, source);

            request.Headers.TryAddWithoutValidation("Accept", "text/html");
            request.Headers.TryAddWithoutValidation("User-Agent", 
                "Mozilla/5.0 (Windows NT 6.1; WOW64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/46.0.2490.80 Safari/537.36");

            var response = await client.SendAsync(request);

            return response;
        }
    }
}
