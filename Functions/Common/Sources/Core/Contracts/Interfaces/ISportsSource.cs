using System.Collections.Generic;
using NuGets.NuGets.Contracts;
using NuGets.NuGets.Contracts.Enums;
using NuGets.NuGets.Dtos.Enums;

namespace Common.Sources.Core.Contracts.Interfaces
{
    public interface ISportsSource
    {
        public SourceConnectorType Type { get; }

        public SourceType SourceType { get; }

        public List<SportType> GetCapabilities();

        public ISportsRetriever GetRetriever(SyncMessage syncMessage);
    }
}
