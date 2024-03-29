﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using Common.Sources.Core.Contracts.Interfaces;
using NuGets.NuGets.Contracts;
using NuGets.NuGets.Contracts.Enums;
using NuGets.NuGets.Dtos.Enums;

namespace Common.Sources.Implementations.SuperPlacar
{
    public class SuperPlacarSource: ISportsSource
    {
        private readonly HttpClient client;

        public SuperPlacarSource(HttpClient client)
        {
            this.client = client;
        }

        //public Uri SourceUri => new Uri("https://superplacar.com.br/");

        public SourceConnectorType Type => SourceConnectorType.SuperPlacar;

        public SourceType SourceType => SourceType.Html;

        public List<SportType> GetCapabilities()
        {
            return new List<SportType>
            {
                SportType.Soccer
            };
        }

        //can use reflection here to eliminate violation of open/closed or interfaces
        public ISportsRetriever GetRetriever(SyncMessage message) => throw new NotImplementedException();
    }
}
