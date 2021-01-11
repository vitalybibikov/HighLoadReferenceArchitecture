using System;
using NuGets.NuGets.Contracts.Enums;
using NuGets.NuGets.Dtos.Enums;

namespace NuGets.NuGets.Contracts
{
    public class SyncMessage
    {
        public SourceConnectorType ConnectorType { get; set; }

        public SportType SportType { get; set; }

        public DateTime When { get; set; }
    }
}
