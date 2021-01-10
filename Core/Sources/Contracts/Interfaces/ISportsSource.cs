using System;
using System.Collections.Generic;
using Core.Core.Sports.Enums;
using Core.NuGet.Contracts;
using Core.Sources.Contracts.Enums;

namespace Core.Sources.Contracts.Interfaces
{
    public interface ISportsSource
    {
        public SourceConnectorType Type { get; }

        public SourceType SourceType { get; }

        public List<SportType> GetCapabilities();

        public ISportsRetriever GetRetriever(SyncMessage syncMessage);
    }
}
