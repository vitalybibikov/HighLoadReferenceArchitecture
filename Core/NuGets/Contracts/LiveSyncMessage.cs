using System;

namespace Core.NuGets.Contracts
{
    public class LiveSyncMessage : SyncMessage
    {
        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public Uri Uri { get; set; }

        public int PollingIntervalInSec { get; set; }
    }
}
