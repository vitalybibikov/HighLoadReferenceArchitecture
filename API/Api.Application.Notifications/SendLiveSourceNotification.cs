using System;
using MediatR;
using NuGets.NuGets.Contracts.Enums;
using NuGets.NuGets.Dtos.Enums;

namespace Api.Application.Notifications
{
    public class SendLiveSourceNotification : INotification
    {
        public Uri Uri { get; set; }

        public DateTime When { get; set; }

        public SourceConnectorType ConnectorType { get; set; }

        public SourceType SourceType { get; set; }

        public SportType SportType { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public int PollingIntervalInSec { get; set; }
    }
}
