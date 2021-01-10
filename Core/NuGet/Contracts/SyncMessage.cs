using System;
using Core.Core.Sports.Enums;
using Core.Sources.Contracts.Enums;

namespace Core.NuGet.Contracts
{
    public class SyncMessage
    {
        public SourceConnectorType ConnectorType { get; set; }

        public SportType SportType { get; set; }

        public DateTime When { get; set; }
    }
}
