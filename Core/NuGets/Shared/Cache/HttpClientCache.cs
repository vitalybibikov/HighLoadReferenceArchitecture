using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Core.NuGets.Shared.Cache
{
    /// <summary>
    /// Optimization for Azure Functions HttpClient, that are shared between instances, improves overall performance x20.
    /// </summary>
    public static class HttpClientCache
    {
        public static readonly  Dictionary<Uri, HttpClient> clients = new Dictionary<Uri, HttpClient>();

        public static HttpClient GetOrCreateClient(Uri uri)
        {
            HttpClient client = null;
            if (clients.ContainsKey(uri))
            {
                client = clients[uri];
            }
            else
            {
                client = new HttpClient {BaseAddress = uri};
                clients.Add(uri,client);
            }

            return client;
        }
    }
}
