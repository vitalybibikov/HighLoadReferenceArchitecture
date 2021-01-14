using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace NuGets.NuGets.Cache
{
    /// <summary>
    /// Optimization for Azure Functions HttpClient, that are shared between instances, improves overall performance x20.
    /// </summary>
    public static class HttpClientCache
    {
        public static readonly ConcurrentDictionary<Uri, HttpClient> clients = new ConcurrentDictionary<Uri, HttpClient>();

        public static HttpClient GetOrCreateClient(Uri uri)
        {
            HttpClient client = null;
            if (clients.ContainsKey(uri))
            {
                client = clients[uri];
            }
            else
            {
                client = new HttpClient { BaseAddress = uri };
                clients.TryAdd(uri, client);
            }

            return client;
        }
    }
}
